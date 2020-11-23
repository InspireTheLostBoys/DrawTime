<template>
    <div>
        <v-container fluid v-if="dt_draw != null">
            <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
            <div class="mt-1" v-if="dt_draw!=null&&dt_draw_participants!=null">{{ dt_draw.total_tickets }} tickets
                {{dt_draw_participants.length}}
                persons</div>
            <h4 class="mt-1">Prize value: R{{dt_draw.total_pot}}</h4>
            <v-row no-gutters>
                <v-col cols="6" class="pa-1">
                    <DuplicateDraw ref="DuplicateDraw" :draw="dt_draw" />
                </v-col>
                <v-col v-if="!anyDrawn" cols="6" class="pa-1">
                    <v-btn block color="error" large @click="cancelDraw">Cancel Draw
                    </v-btn>
                </v-col>
                <v-col v-if="!anyDrawn" cols="6" class="pa-1">
                    <v-btn large color="warning" @click="$router.push('/Participants/' + dt_draw.id)" block>edit</v-btn>
                </v-col>

                <v-col v-if="!drawStarted" cols="6" class="pa-1">
                    <v-btn large block color="success" @click="sendDrawControl('SHOW',true)">
                        show draw
                    </v-btn>
                </v-col>
                <v-col v-if="drawStarted" cols="6" class="pa-1">
                    <v-btn large block color="success" @click="sendDrawControl('HIDE',false)">
                        Hide draw
                    </v-btn>
                </v-col>
                <v-col v-if="drawStarted" cols="6" class="pa-1">
                    <v-btn large block color="warning" @click="sendDrawControl('RESET',true)">
                        reset display
                    </v-btn>
                </v-col>
                <v-col v-if="allDrawn && !dt_draw.completed" cols="12" class="pa-1">
                    <v-btn large block color="success" @click="completeDraw(true)">
                        mark draw as complete
                    </v-btn>
                </v-col>
                <v-col v-if="dt_draw.completed" cols="12" class="pa-1">
                    <v-btn large block color="warning" @click="completeDraw(false)">
                        mark draw as incomplete
                    </v-btn>
                </v-col>
            </v-row>

            <div v-if="dt_draw!=null" v-for="(prize,idx) in dt_draw.prizes" :key="idx">
                <v-card @click="drawPrize(prize)" v-if="prize.winner_id==0" class="ma-2" color="primary" dark>
                    <div class="d-flex flex-no-wrap justify-space-between">
                        <div>
                            <v-card-title class="headline"> Draw
                                for:</v-card-title>
                            <v-card-subtitle  v-if="prize.show_value" cols="12" no-gutters>

                                {{prize.description}} - R{{prize.prize_value}}
                            </v-card-subtitle>
                            <v-card-subtitle  v-if="prize.percentage_of_pot" cols="12" no-gutters>

                                {{prize.description}} - {{prize.pot_percentage}}% of Pot
                            </v-card-subtitle>
                        </div>
                        <v-avatar class="ma-3" size="125" tile>
                            <object height="100" width="100%" :data="getImg(prize)" type="image/png">
                                <img height="100%" src="img/no-image.png" style="width:100% !important">
                            </object>
                        </v-avatar>
                    </div>
                </v-card>

                <v-card class="ma-2" v-else block color="grey lighten-1" justify-center align-center>
                    <div class="d-flex flex-no-wrap justify-space-between">
                        <div>
                             <v-card-title class="headline"> Prize DRAWN :</v-card-title>
                            <v-card-subtitle v-if="prize.show_value" cols="12" no-gutters >
                                {{prize.description}} - R{{prize.prize_value}}
                            </v-card-subtitle>
                            <v-card-subtitle v-if="prize.percentage_of_pot" cols="12" no-gutters >
                                 {{prize.description}} - {{prize.pot_percentage}}% of Pot
                            </v-card-subtitle>

                            <v-col v-if="dt_draw_participants!=null" cols="12" >
                                Winner : {{GetDrawWinner(prize)}}
                            </v-col>
                        </div>
                        <v-avatar class="ma-3" size="125" tile>
                            <object height="100" width="100%" :data="getImg(prize)" type="image/png">
                                <img height="100%" src="img/no-image.png" style="width:100% !important">
                            </object>
                        </v-avatar>
                    </div>
                </v-card>
                <!-- draw prizes -->
                <!-- <div v-if="prize.winner_id==0">
                    <v-row no-gutters>
                        <v-col no-gutters cols="3">
                            <v-card class="my-3" height="100" width="100%">
                                <object height="100" width="100%" :data="getImg(prize)" type="image/png">
                                    <img height="100%" src="img/no-image.png" style="width:100% !important">
                                </object>
                            </v-card>
                        </v-col>
                        <v-col no-gutters cols="9">
                            <v-card v-if="prize.show_value" dark class="my-3" large block color="primary"
                                @click="drawPrize(prize)">
                                </v-card>
                            <v-card v-if="prize.percentage_of_pot" class="my-3" large dark block color="primary"
                                @click="drawPrize(prize)"> </v-card>
                        </v-col>
                    </v-row>
                </div> -->

                <!-- completed card -->

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
                allDrawn: true,
                drawStarted: false

            }
        },
        mounted() {
            let self = this
            self.getDrawDetails();
            self.initWebsocket()
        },
        methods: {
            getImg(item) {
                let self = this

                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/Prizes/" + item.id + ".png"
            },
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
                    } else {
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
                        afterReturn => {})
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
            sendDrawControl(type, drawStarted) {
                let self = this
                let DrawMsg = {
                    "MsgType": "STAGEGAMES",
                    "GameType": "DRAWTIME",
                    "Function": "READYDRAW",
                    "Language": "DE",
                    "Translate": ["Welcome", "Mobile Number"],
                    "Draw_ID": self.dt_draw.id,
                    "Action": type
                }
                console.log("ddrawmsg", DrawMsg);
                self.socket.sendMessage(DrawMsg, self.onMessage)
                self.drawStarted = drawStarted
            }
        },
    }
</script>