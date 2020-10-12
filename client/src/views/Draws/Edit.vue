<template>
    <div>
         
        <v-container fluid class="fill-height" v-if="draw != null">
            <v-row no-gutters>
                <v-col cols="12">
                    <label>Draw name</label>
                    <v-text-field v-model="draw.draw_name" dense outlined></v-text-field>
                </v-col>
                <v-col cols="12">
                    <label>Draw type</label>
                    <v-select v-model="draw.draw_type" :items="drawTypes" dense outlined></v-select>
                </v-col>
                <v-col cols="12">
                    <label>Issue open</label>
                    <datetime type="datetime" v-model="draw.issue_open" class="input"></datetime>
                </v-col>
                <v-col cols="12">
                    <label>Issue close</label>
                    <datetime type="datetime" v-model="draw.issue_close" class="input"></datetime>
                </v-col>
                <v-col cols="12">
                    <label>Draw time</label>
                    <datetime type="datetime" v-model="draw.draw_time" class="input"></datetime>
                </v-col>
                <v-col cols="12" v-if="draw.draw_type == 2">
                    <label>Ticket cost</label>
                    <v-text-field v-model="draw.ticket_cost" prefix="R" dense outlined></v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12">
                    <PrizeList :editPrize="editPrize" :addPrize="addPrize" :prizes="draw.prizes"
                        :deletePrize="deletePrize" />
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12">
                    <v-btn @click="updateDraw" block color="primary" large>Update Draw</v-btn>
                </v-col>
                <!-- <v-col cols="12" class="pt-0">
                    <v-btn block color="primary" large>
                        <div>
                            <div>Entries</div>
                            <div>(239 tickets 82 persons)</div>
                        </div>
                    </v-btn>
                </v-col> -->
                <v-col cols="12" class="pt-0">
                    <v-btn block color="error" large @click="cancelDraw">Cancel Draw</v-btn>
                </v-col>
                <v-col cols="12" @click="$router.go(-1)" class="pt-0">
                    <v-btn block large>Cancel </v-btn>
                </v-col>
            </v-row>
        </v-container>
        <Participants ref="participantsModal" />
        <prizeModal ref="prizeModal" />
        <YesNoModal ref="YesNoModal" />
    </div>
</template>

<script>
    import PrizeList from './components/prize-list.vue'
    import Participants from './components/participants-modal.vue'
    import prizeModal from './components/prize-modal.vue'
    import YesNoModal from '@/components/YesNoModal.vue'



    import {
        Datetime
    } from 'vue-datetime';

    export default {
        components: {
            PrizeList,
            Datetime,
            Participants,
            prizeModal,
            YesNoModal
        },
        data() {
            return {
                draw: null,
                drawTypes: [{
                        text: 'No entry fee',
                        value: 1,
                    },
                    {
                        text: 'Entry fee',
                        value: 2,
                    }
                ]
            }
        },
        mounted() {
            let self = this;
            self.getDraw();
        },
        methods: {
            cancelDraw() {
                let self = this
                self.$refs.YesNoModal.show("error", "Cancel Draw", "Are you sure you wish to cancel this draw?",
                    value => {
                        if (value) {
                            self.draw.cancelled = true
                            self.put('dt_draw', self.draw)
                                .then(r => {
                                    self.$router.push('/Landing')
                                })
                        }
                    })

            },
            editPrize(item) {
                let self = this
                self.$refs.prizeModal.show(item, newPrize => {
                    let index = self.draw.prizes.indexOf(item)
                    let addablePrize = JSON.parse(JSON.stringify(newPrize))
                    for (var prop in self.draw.prizes[index]) {
                        self.draw.prizes[index][prop] = addablePrize[prop]
                    }
                })
            },
            deletePrize(item) {
                let self = this
                self.$refs.YesNoModal.show("error", "Delete Prize", "Are you sure you wish to delete this prize?",
                    value => {
                        if (value) {
                            self.draw.prizes.splice(self.draw.prizes.indexOf(item), 1)
                        }
                    })
            },
            addPrize() {
                let self = this
                if (self.draw.prizes == null) {
                    self.draw.prizes = []
                }
                let prize = {
                    id: 0,
                    draw_id: self.draw.id,
                    description: "",
                    sequence_no: 1,
                    show_value: true,
                    percentage_of_pot: false,
                    pot_percentage: 0,
                    prize_value: 0,
                }

                self.$refs.prizeModal.show(prize, newPrize => {
                    let addablePrize = JSON.parse(JSON.stringify(newPrize))
                    self.draw.prizes.push(addablePrize)
                })
            },
            getDraw() {
                let self = this;
                let drawID = self.$route.params.draw_id;
                self.get('dt_draw/' + drawID)
                    .then(r => {
                        self.draw = r.data.dt_draw;
                    })
            },
            updateDraw() {
                let self = this;
                self.put('dt_draw', self.draw)
                    .then(r => {
                        self.$router.push('/Landing')
                    })
            }
        }
    }
</script>

<style scoped>
    .input {
        height: 40px;
        width: 100%;
        border: 1px solid #9e9e9e;
        border-radius: 5px;
        padding-left: 10px;
        padding-top: 7px;
        margin-bottom: 20px;
    }
</style>