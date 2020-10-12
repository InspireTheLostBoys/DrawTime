<template>
    <div>
         
        <v-container fluid class="fill-height">
            <v-row no-gutters>
                <v-col cols="12">
                    <label>Draw name</label>
                    <v-text-field v-model="drawName" dense outlined></v-text-field>
                </v-col>
                <v-col cols="12">
                    <label>Draw type</label>
                    <v-select v-model="drawType" :items="drawTypes" dense outlined></v-select>
                </v-col>
                <v-col cols="12">
                    <label>Issue open</label>
                    <datetime type="datetime" v-model="issueOpen" class="input"></datetime>
                </v-col>
                <v-col cols="12">
                    <label>Issue close</label>
                    <datetime type="datetime" v-model="issueClose" class="input"></datetime>
                </v-col>
                <v-col cols="12">
                    <label>Draw time</label>
                    <datetime type="datetime" v-model="drawTime" class="input"></datetime>
                </v-col>
                <v-col cols="12" v-if="drawType == 2">
                    <label>Ticket cost</label>
                    <v-text-field v-model="ticketCost" prefix="R" dense outlined></v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12">
                    <!-- <PrizeList /> -->
                </v-col>
            </v-row>
            <v-row>
                <!-- <v-col cols="12">
                    <v-btn block color="primary" large>Update Draw</v-btn>
                </v-col>
                <v-col cols="12" class="pt-0">
                    <v-btn block color="primary" large @click="viewParticipants">
                        <div>
                            <div>Entries</div>
                            <div>(239 tickets 82 persons)</div>
                        </div>
                    </v-btn>
                </v-col> -->
                <v-col cols="12" class="pt-0">
                    <v-btn @click="createDraw" block color="success" large>Create Draw</v-btn>
                </v-col>
            </v-row>
        </v-container>
        <Participants ref="participantsModal" />
    </div>
</template>

<script>
    import PrizeList from './components/prize-list.vue'
    import Participants from './components/participants-modal.vue'

    import {
        Datetime
    } from 'vue-datetime';

    export default {
        components: {
            PrizeList,
            Datetime,
            Participants
        },
        data() {
            return {
                step: 1,
                drawName: '',
                drawType: 1,
                issueOpen: '',
                issueClose: '',
                drawTime: '',
                entryFee: false,
                ticketCost: 0,
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
        methods: {
            createDraw() {
                let self = this;

                let request = {
                    draw_name: self.drawName,
                    issue_open: self.issueOpen,
                    issue_close: self.issueClose,
                    draw_time: self.drawTime,
                    draw_type: self.drawType,
                    entry_fee: self.drawType == 2,
                    ticket_cost: self.ticketCost
                }

                self.post('dt_draw', request)
                    .then(r => {
                        self.$router.push('/Landing')
                    })
                    .catch(e => {
                        alert("Failed to create draw");
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