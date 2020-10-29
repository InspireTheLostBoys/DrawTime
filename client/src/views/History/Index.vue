<template>
    <v-container fluid class="fill-height">
        <v-row>
            <v-col cols="12" v-if="draws.length==0">
                <v-card>
                    <v-card-text>
                        There are currently no completed draws
                    </v-card-text>
                </v-card>
            </v-col>
            <v-col cols="12" v-for="(draw, idx) in draws" :key="idx">
                <v-card @click="routeToHistory(draw)">
                    <v-card-text>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <h3>{{ draw.draw_name }}</h3>
                                    <div>{{FormatDateTime(draw.draw_time)}}</div>
                                    <div>
                                        <!-- <v-chip label x-small color="primary">
                                            Issue Closed
                                        </v-chip> -->
                                        <v-chip v-if="draw.completed" label x-small color="success">
                                            Completed
                                        </v-chip>
                                        <v-chip v-if="draw.cancelled" label x-small color="error">
                                            Cancelled
                                        </v-chip>
                                        <!-- <v-chip class="ml-1" label x-small color="primary">
                                            Draw Ready
                                        </v-chip> -->
                                    </div>
                                </td>
                                <td style="text-align: right;">
                                    <div>{{draw.total_tickets}} tickets</div>
                                    <div>Tickets cost: R{{ draw.ticket_cost }}</div>
                                    <div>
                                        <b>Pot: {{draw.total_pot}}</b>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </v-card-text>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
    export default {
        data() {
            return {
                draws: [],
                userAccess: null,
            }
        },
        mounted() {
            this.getDraws();
            this.getUserAccess()
        },
        methods: {
            canPlay(draw) {
                let self = this
                self.get(`dt_draw/CanPlay?draw_id=${draw.id}`).then(r => {
                    if (r.data) {
                        if (self.userAccess.role_id != 9) {
                            self.$router.push('/DrawStart/' + draw.id)
                        }
                    } else {
                        self.$router.push('/Participants/' + draw.id)
                    }
                })
            },
            getUserAccess() {
                let self = this
                console.log(localStorage.getItem("userDetails"));

                self.userAccess = JSON.parse(localStorage.getItem("userDetails"));
            },
            routeToHistory(draw) {
                let self = this
                if (self.userAccess.role_id != 9) {
                    self.$router.push('/History/' + draw.id)
                }
                // 
            },

            getDraws() {
                let self = this;

                self.get('dt_draw/History')
                    .then(r => {
                        console.log("draws/History", r.data);

                        self.draws = r.data;
                    })
            },
        }
    }
</script>