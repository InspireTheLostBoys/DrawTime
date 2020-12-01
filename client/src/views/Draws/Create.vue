<template>
    <div>
        <v-container fluid class="fill-height">
            <v-row no-gutters justify="center">
                <v-col class="pa-1" cols="12" lg="6" md="6">
                    <label>Draw name</label>
                    <v-text-field v-model="drawName" dense outlined></v-text-field>
                </v-col>
                <v-col class="pa-1" cols="12" lg="6" md="6">
                    <label>Draw type</label>
                    <v-select v-model="drawType" :items="drawTypes" dense outlined></v-select>
                </v-col>
                <v-col class="pa-1" cols="12" lg="6" md="6">
                    <label>Issue open</label>
                    <datetime type="datetime" :minute-step="15" :max-datetime="issueClose" v-model="issueOpen"
                        class="input"></datetime>
                </v-col>
                <v-col class="pa-1" cols="12" lg="6" md="6">
                    <label>Issue close</label>
                    <datetime type="datetime" :minute-step="15" :min-datetime="issueOpen" v-model="issueClose"
                        class="input"></datetime>
                </v-col>
                <v-col class="pa-1" cols="12" lg="6" md="6">
                    <label>Draw time</label>
                    <datetime type="datetime" :minute-step="15" v-model="drawTime" :min-datetime="issueClose"
                        class="input"></datetime>
                </v-col>
                <v-col class="pa-1" cols="12" lg="6" md="6" v-if="drawType == 2">
                    <label>Ticket cost</label>
                    <v-text-field v-model="ticketCost" prefix="R" dense outlined></v-text-field>
                </v-col>
            </v-row>
            <v-row justify="center">
                <v-col cols="12" lg="6" md="6" class="pt-0">
                    <v-btn @click="validate" block color="success" large>Create Draw</v-btn>
                </v-col>
            </v-row>
        </v-container>
        <Participants ref="participantsModal" />
        <ErrorDialog ref="ErrorDialog" />
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
            validate() {
                let self = this
                let valid = true
                if (self.drawName == "" || self.issueOpen == "" || self.issueClose == "" || self.drawTime == "") {
                    self.$refs.ErrorDialog.show("Please ensure all draw details are filled in",
                        val => {})
                } else {
                    console.log(self.drawType == 2 && parseFloat(self.ticketCost) <= 0,self.drawType);
                    
                    if (self.drawType == 2 && parseFloat(self.ticketCost) <= 0) {
                        self.$refs.ErrorDialog.show("Ticket Cost must be greater than 0",
                            val => {})
                    } else {
                        self.createDraw()
                    }
                }
            },
            createDraw() {
                let self = this;
                let request = {
                    draw_name: self.drawName,
                    issue_open: self.issueOpen,
                    issue_close: self.issueClose,
                    draw_time: self.drawTime,
                    draw_type: self.drawType,
                    entry_fee: self.drawType == 2,
                    ticket_cost: self.ticketCost,
                    cancelled: false,
                    completed: false,
                }
                request.issue_open = self.FormatDateTime(request.issue_open)
                request.issue_close = self.FormatDateTime(request.issue_close)
                request.draw_time = self.FormatDateTime(request.draw_time)
                self.post('dt_draw', request)
                    .then(r => {
                        console.log("create DRAW", r);
                        self.$router.push('/EditDraw/' + r.data.dt_draw.id)
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

        border: 1px solid #9e9e9e;
        border-radius: 5px;
        padding-left: 10px;
        padding-top: 7px;
        margin-bottom: 20px;
    }
</style>