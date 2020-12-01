<template>
    <div>
        <v-container fluid v-if="dt_draw != null">
            <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
            <div class="mt-1" v-if="dt_draw!=null&&dt_draw_participants!=null">{{ dt_draw.total_tickets }} tickets
                {{dt_draw_participants.length}}
                persons</div>
            <h4 class="mt-1">Prize value: R{{dt_draw.total_pot}}</h4>
            <DuplicateDraw ref="DuplicateDraw" :draw="dt_draw" />
            <v-btn class="my-3" v-if="!dt_draw.cancelled && dt_draw.completed" large block color="warning"
                @click="completeDraw(false)">
                mark draw as incomplete
            </v-btn>
            <v-btn class="my-3" v-if="dt_draw.cancelled " large block color="warning" @click="unCancelDraw(false)">
                Mark draw as not cancelled
            </v-btn>
            <v-btn class="my-3" v-if="userAccess.role_id == 1" large block color="error" @click="deleteFromHistory()">
                Delete from history
            </v-btn>
            <v-row>
                <v-col cols="12" xs="12" sm="12" md="6" lg="6" xl="6" v-if="dt_draw!=null"
                    v-for="(prize,idx) in dt_draw.prizes" :key="idx">
                     <v-card v-if="prize.winner_id!=0"  block color="grey lighten-1" justify-center align-center>
                        <div class="d-flex flex-no-wrap justify-space-between">
                            <div>
                                <v-card-title class="headline"> Prize DRAWN :</v-card-title>
                                <v-card-subtitle v-if="prize.show_value" cols="12" no-gutters>
                                    {{prize.description}} - R{{prize.prize_value}}
                                </v-card-subtitle>
                                <v-card-subtitle v-if="prize.percentage_of_pot" cols="12" no-gutters>
                                    {{prize.description}} - {{prize.pot_percentage}}% of Pot
                                </v-card-subtitle>
                                <v-col v-if="dt_draw_participants!=null" cols="12">
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
                </v-col>
            </v-row>
            <YesNoModal ref="YesNoModal" />
            <ErrorDialog ref="ErrorDialog" />
        </v-container>
    </div>
</template>
<script>
    import YesNoModal from '@/components/YesNoModal.vue'
    import ErrorDialog from '@/components/Common/Overlays/Dialogs/Error.vue'

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
                userAccess: null,

            }
        },
        mounted() {
            let self = this
            self.getDrawDetails();
            self.getUserAccess()
        },
        methods: {
              getImg(item) {
                let self = this

                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/Prizes/" + item.id + ".png"
            },
            getUserAccess() {
                let self = this
                console.log(localStorage.getItem("userDetails"));

                self.userAccess = JSON.parse(localStorage.getItem("userDetails"));
            },
            deleteFromHistory() {
                let self = this
                self.$refs.YesNoModal.show("error", "Delete Draw",
                    "Are you sure you wish to permanently delete this draw?", val => {
                        if (val) {
                            self.delete(`dt_draw?draw_id=${self.dt_draw.id}`).then(r => {
                                // self.$router.push("/History")
                                self.$router.back()
                            })
                        }
                    })
            },
            unCancelDraw(val) {
                let self = this
                self.dt_draw.cancelled = val
                self.put('dt_draw', self.dt_draw).then(r => {
                    self.$router.push('/Landing')
                })
            },
            completeDraw(completed) {
                let self = this
                self.dt_draw.completed = completed
                self.put('dt_draw', self.dt_draw).then(r => {
                    self.$router.push('/Landing')
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
                    })
            },
        }
    }
</script>