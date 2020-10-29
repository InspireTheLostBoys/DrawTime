<template>
    <div>
        <v-container fluid v-if="dt_draw != null">
            <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
            <div class="mt-1" v-if="dt_draw!=null&&dt_draw_participants!=null">{{ dt_draw.total_tickets }} tickets
                {{dt_draw_participants.length}}
                persons</div>
            <h4 class="mt-1">Prize value: R{{dt_draw.total_pot}}</h4>
            <DuplicateDraw ref="DuplicateDraw" :draw="dt_draw" />
            <v-btn class="mt-3" v-if="!anyDrawn" block color="error" large @click="cancelDraw">Cancel Draw</v-btn>
            <v-btn class="mt-3" v-if="!anyDrawn" large color="warning" @click="$router.push('/Participants/' + dt_draw.id)" block>edit</v-btn>
            <v-btn class="my-3" v-if="allDrawn && !dt_draw.completed" large block color="success"
                @click="completeDraw(true)">
                mark draw as complete
            </v-btn>
            
            <v-btn class="my-3" v-if="dt_draw.completed" large block color="warning" @click="completeDraw(false)">
                mark draw as incomplete
            </v-btn>
            <div v-if="dt_draw!=null" v-for="(prize,idx) in dt_draw.prizes" :key="idx">

                <!-- draw prizes -->
                <div v-if="prize.winner_id==0">
                    <v-btn v-if="prize.show_value" class="my-3" large block color="primary" @click="drawPrize(prize)">
                        Draw
                        for
                        {{prize.description}} - R{{prize.prize_value}} </v-btn>
                    <v-btn v-if="prize.percentage_of_pot" class="my-3" large block color="primary"
                        @click="drawPrize(prize)">Draw for
                        {{prize.description}} - {{prize.pot_percentage}}% of Pot </v-btn>
                </div>

                <!-- completed card -->
                <v-card class="ma-5" v-else block color="grey lighten-1" justify-center align-center>
                    <v-container no-gutters justify-center align-center>
                        <v-row no-gutters>
                            <v-col v-if="prize.show_value" cols="12" no-gutters style="text-align: center;">
                                Prize DRAWN : {{prize.description}} - R{{prize.prize_value}}
                            </v-col>
                            <v-col v-if="prize.percentage_of_pot" cols="12" no-gutters style="text-align: center;">
                                Prize DRAWN : {{prize.description}} - {{prize.pot_percentage}}% of Pot
                            </v-col>

                            <v-col v-if="dt_draw_participants!=null" cols="12" style="text-align: center;">
                                Winner : {{GetDrawWinner(prize)}}
                            </v-col>
                        </v-row>
                    </v-container>
                </v-card>
            </div>
            <YesNoModal ref="YesNoModal" />
            <ErrorDialog ref="ErrorDialog" />
        </v-container>
    </div>
</template>
<script>
    import YesNoModal from '@/components/YesNoModal.vue'
    import ErrorDialog from '@/components/Common/Overlays/Dialogs/Error.vue'
    import websocketClient from '@/libs/websocket-client'

    export default {
        components: {
            YesNoModal,
            ErrorDialog
        },
        data() {
            return {
                dt_draw: null,
                socket: null,
                dt_draw_participants: null,
                allDrawn: true

            }
        },
        mounted() {
            let self = this
            self.getDrawDetails();
            self.initWebsocket()
        },
        methods: {
            cancelDraw() {
                let self = this
                self.$refs.YesNoModal.show("error", "Cancel Draw", "Are you sure you wish to cancel this draw?",
                    value => {
                        if (value) {
                            self.dt_draw.cancelled = true
                            self.put('dt_draw', self.dt_draw)
                                .then(r => {
                                    self.$router.push('/Landing')
                                })
                        }
                    })

            },
            completeDraw(completed) {
                let self = this
                self.dt_draw.completed = completed
                self.put('dt_draw', self.dt_draw).then(r => {
                    self.getDrawDetails()
                })
            },
            checkAllPrizes() {
                let self = this
                self.allDrawn = true
                self.anyDrawn = false
                self.dt_draw.prizes.forEach(prize => {
                    if (prize.winner_id == 0) {
                        self.allDrawn = false
                    }else{
                        self.anyDrawn = true
                    }
                })
            },
            promptRedraw(item) {
                let self = this
                self.$refs.YesNoModal.show("warning", "Redraw " + item.description,
                    "Would you like to redraw this Prize?", value => {
                        console.log(value);

                        if (value) {
                            let DrawMsg = {
                                "MsgType": "STAGEGAMES",
                                "GameType": "DRAWTIME",
                                "Function": "DRAWPRIZE",
                                "Language": "DE",
                                "Translate": ["Welcome", "Mobile Number"],
                                "Draw_ID": self.dt_draw.id,
                                "Draw_Prize_ID": item.id
                            }
                            console.log("ddrawmsg", DrawMsg);

                            self.socket.sendMessage(DrawMsg, self.onMessage)
                        } else {

                        }
                    })
            },
            GetDrawWinner(prize) {
                let self = this
                let winner = self.dt_draw_participants.find(x => x.id == prize.winner_id)
                if (winner != null) {
                    return winner.display_name + " - " + winner.reference
                } else {
                    return ""
                }
            },
            getDrawParticipants() {
                let self = this;
                let draw_id = self.$route.params.draw_id;
                self.get('dt_draw_participant/' + draw_id)
                    .then(r => {
                        console.log("getDrawParticipants", r.data);
                        self.dt_draw_participants = r.data.dt_draw_participants;
                    })
            },
            getDrawDetails() {
                let self = this;
                let draw_id = self.$route.params.draw_id;
                self.get('dt_draw/' + draw_id)
                    .then(r => {
                        console.log(r);
                        self.dt_draw = r.data.dt_draw;
                        self.getDrawParticipants()
                        self.checkAllPrizes()
                    })
            },
            logError() {
                let self = this
                self.$refs.ErrorDialog.show("Failed to connect to Websocket",
                    afterReturn => {})
            },
            onConnect() {
                let self = this
                console.log("connected");

            },
            onMessage(data) {
                let self = this
                console.log("onMessage", data);
                if (!data.Error) {
                    self.getDrawDetails()
                } else {
                    self.$refs.ErrorDialog.show("Error occured with draw",
                        afterReturn => {

                        })
                }

            },
            initWebsocket() {
                let self = this
                let params = {
                    wsAddress: 'ws://192.168.1.177:17501',
                    onConnect: self.onConnect,
                    onMessage: self.onMessage,
                    onError: self.logError
                }
                self.socket = new websocketClient(params);
            },
            drawPrize(item) {
                let self = this
                self.$refs.YesNoModal.show("success", "Draw Prize: " + item.description,
                    "Would you like to draw this Prize?", value => {
                        if (value) {
                            let DrawMsg = {
                                "MsgType": "STAGEGAMES",
                                "GameType": "DRAWTIME",
                                "Function": "DRAWPRIZE",
                                "Language": "DE",
                                "Translate": ["Welcome", "Mobile Number"],
                                "Draw_ID": self.dt_draw.id,
                                "Draw_Prize_ID": item.id
                            }
                            console.log("ddrawmsg", DrawMsg);

                            self.socket.sendMessage(DrawMsg, self.onMessage)

                        }
                    })
            },
        }
    }
</script>