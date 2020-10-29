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
            <v-btn class="my-3"  large block color="error" @click="deleteFromHistory()">
                Delete from history
            </v-btn>
            <div v-if="dt_draw!=null && dt_draw_participants!=null" v-for="(prize,idx) in dt_draw.prizes" :key="idx">

                <v-card class="ma-5" block color="grey lighten-1" justify-center align-center>
                    <v-container no-gutters justify-center align-center>
                        <v-row no-gutters>
                            <v-col v-if="prize.show_value" cols="12" no-gutters style="text-align: center;">
                                Prize DRAWN : {{prize.description}} - R{{prize.prize_value}}
                            </v-col>
                            <v-col v-if="prize.percentage_of_pot" cols="12" no-gutters style="text-align: center;">
                                Prize DRAWN : {{prize.description}} - {{prize.pot_percentage}}% of Pot
                            </v-col>
                            <v-col cols="12" style="text-align: center;">
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
        },
        methods: {
            deleteFromHistory(){
                let self = this
                self.$refs.YesNoModal.show("error", "Delete Draw", "Are you sure you wish to permanently delete this draw?",val=>{
                    if(val){
                        self.delete(`dt_draw?draw_id=${self.dt_draw.id}`).then(r=>{
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