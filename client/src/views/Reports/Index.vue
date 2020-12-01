<template>
    <div>
        <v-container fluid class="fill-height">
            <v-row>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3">
                    <label> Report type</label>
                    <v-autocomplete @change="setHeaders" dense hide-details outlined :items="reportTypes"
                        v-model="selectedReportType">

                    </v-autocomplete>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3">
                    <label> Report type</label>
                    <v-btn @click="runReport" :disabled="startPeriod==''||endPeriod==''">
                        run report
                    </v-btn>
                </v-col>
            </v-row>
            <v-row no-gutters>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3">
                    <label> Report start</label>
                    <datetime type="date" :minute-step="15" :max-datetime="endPeriod" v-model="startPeriod"
                        class="input"></datetime>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3">
                    <label> Report end</label>
                    <datetime type="date" :minute-step="15" :min-datetime="startPeriod" v-model="endPeriod"
                        class="input">
                    </datetime>
                </v-col>
            </v-row>
            <v-row>

            </v-row>
            <v-row no-gutters justify="center">
                <table style="width: 100%;" class="blueTable">
                    <tr style="text-align: left">
                        <th v-for="(item,idx) in headers" :key="idx"> {{item.text}}</th>
                    </tr>
                    <tr v-if="reportData!=null" v-for="(item,idx) in reportData" :key="idx">
                        <td v-for="(head,idx) in headers" :key="idx">{{item[head.value]}}</td>
                    </tr>
                </table>
            </v-row>
        </v-container>
    </div>
</template>


<script>
    import {
        Datetime
    } from 'vue-datetime';
    export default {
        components: {
            Datetime
        },
        data() {
            return {
                participation_perc: 0.5,
                headers: [{
                        text: "Name",
                        value: "draw_name"
                    },
                    {
                        text: "Total Tickets",
                        value: "sum"
                    },
                    {
                        text: "Ticket Cost",
                        value: "ticket_cost"
                    },
                    {
                        text: "Total Ticket Sales",
                        value: "total_tickets_value"
                    },
                ],
                selectedReportType: 0,
                reportTypes: [],
                config: null,
                reportData: [],
                startPeriod: null,
                endPeriod: null,
                userAccess: null,
            }
        },
        mounted() {
            let self = this
            self.userAccess = JSON.parse(localStorage.getItem("userDetails"));

            this.getSystemConfig()
            this.setReportTypes()
        },
        methods: {
            setReportTypes() {
                let self = this
                if (self.userAccess.role_id == 1 || self.userAccess.role_id == 0) {
                    self.reportTypes = [{
                        text: "Date Report",
                        value: 0
                    }, {
                        text: "Ticket Sales Report",
                        value: 1
                    }, {
                        text: "Reseller Report",
                        value: 2
                    }]
                } else {
                    self.reportTypes = [{
                        text: "Date Report",
                        value: 0
                    }, {
                        text: "Ticket Sales Report",
                        value: 1
                    }]
                }
            },
            getSystemConfig() {
                let self = this;
                self.get('dt_config')
                    .then(r => {
                        self.config = r.data.dt_config;
                        if (self.config.participation_perc == null || self.config.participation_perc == undefined) {
                            self.participation_perc = 0
                        }
                    })
            },
            setHeaders() {
                let self = this
                self.reportData = []
                self.$nextTick(() => {
                    switch (self.selectedReportType) {
                        case 0: {
                            self.headers = [{
                                    text: "Name",
                                    value: "draw_name"
                                },
                                {
                                    text: "Total Tickets",
                                    value: "sum"
                                },
                                {
                                    text: "Ticket Cost",
                                    value: "ticket_cost"
                                },
                                {
                                    text: "Total Ticket Sales",
                                    value: "total_tickets_value"
                                },
                            ]
                        }
                        break;
                    case 1: {
                        self.headers = [{
                                text: "User",
                                value: "username"
                            },
                            {
                                text: "Draw Name",
                                value: "draw_name"

                            },
                            {
                                value: "sum",
                                text: "Total Tickets"
                            },
                            {
                                value: "ticket_cost",
                                text: "Ticket Cost"
                            },
                            {
                                text: " Total Value",
                                value: "total_tickets_value"
                            },

                        ]
                    }
                    break;
                    case 2: {
                        self.headers = [{
                                text: "Draw",
                                value: "draw_name"
                            },
                            {
                                text: "Prize Cost",
                                value: "prize_cost",

                            },
                            {
                                text: "Prize value",
                                value: "prize_value",

                            },
                            {
                                text: "Ticket Sales",
                                value: "total_ticket_value"
                            }, {
                                text: "Reseller Cut",
                                value: "reseller_amount"
                            }

                        ]
                    }
                    break;
                    default:
                        break;
                    }
                })
            },
            runReport() {
                let self = this
                self.reportData = []
                switch (self.selectedReportType) {
                    case 0: {
                        self.post(
                                `reports/Period?period_start=${self.startPeriod}&period_end=${self.endPeriod}`)
                            .then(
                                r => {
                                    console.log(r.data);
                                    r.data.forEach(item => {
                                        item.total_tickets_value = item.sum * item.ticket_cost
                                        self.reportData.push(item)
                                    })


                                })
                    }
                    break;
                case 1: {
                    self.post(
                            `reports/Tickets?period_start=${self.FormatDate(self.startPeriod)}&period_end=${self.FormatDate(self.endPeriod)}`
                        )
                        .then(
                            r => {
                                console.log(r.data);
                                r.data.forEach(item => {
                                    item.total_tickets_value = item.sum * item.ticket_cost
                                    self.reportData.push(item)
                                })
                            })
                }
                break;

                case 2: {
                    self.post(
                            `reports/Reseller?period_start=${self.FormatDate(self.startPeriod)}&period_end=${self.FormatDate(self.endPeriod)}`
                        )
                        .then(
                            r => {
                                console.log(r.data);
                                let UnformattedWithSummedCost = []
                                r.data.forEach(item => {
                                    if (item.pot_percentage > 0) {
                                        let newItem = {
                                            draw_name: item.draw_name,
                                            prize_cost: (item.sum * item.ticket_cost) * (item
                                                .pot_percentage / 100),
                                            prize_value: item.prize_value,
                                            total_ticket_value: item.sum * item.ticket_cost
                                        }
                                        UnformattedWithSummedCost.push(newItem)
                                    } else {
                                        let newItem = {
                                            draw_name: item.draw_name,
                                            prize_cost: item.prize_cost,
                                            prize_value: item.prize_value,
                                            total_ticket_value: item.sum * item.ticket_cost
                                        }
                                        UnformattedWithSummedCost.push(newItem)
                                    }
                                })
                                console.log("UnformattedWithSummedCost", UnformattedWithSummedCost);

                                let formatted = []
                                UnformattedWithSummedCost.forEach(item => {
                                    let foundItem = formatted.find(x => x.draw_name == item.draw_name)
                                    if (foundItem == null) {
                                        formatted.push(item)
                                    } else {
                                        if (foundItem.prize_cost == null) {
                                            foundItem.prize_cost = 0
                                        }
                                        if (item.prize_cost == null) {
                                            item.prize_cost = 0
                                        }
                                        foundItem.prize_cost = foundItem.prize_cost + item.prize_cost
                                        foundItem.prize_value = foundItem.prize_value + item.prize_value
                                    }
                                })
                                console.log("formatted", formatted);
                                formatted.forEach(item => {
                                    item.reseller_amount = (item.total_ticket_value - item.prize_cost) * self
                                        .participation_perc
                                })
                                self.reportData = formatted
                            })
                }
                break;

                default:
                    break;
                }

            },
        }
    }
</script>
<style>
    table.blueTable {
        border: 1px solid #1C6EA4;
        background-color: #EEEEEE;
        width: 100%;
        text-align: left;
        border-collapse: collapse;
    }

    table.blueTable td,
    table.blueTable th {
        border: 1px solid #AAAAAA;
        padding: 3px 2px;
    }

    table.blueTable tbody td {
        font-size: 13px;
    }

    table.blueTable tr:nth-child(even) {
        background: #D0E4F5;
    }

    table.blueTable thead {
        background: #1C6EA4;
        background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
        background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
        background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
        border-bottom: 2px solid #444444;
    }

    table.blueTable thead th {
        font-size: 15px;
        font-weight: bold;
        color: #FFFFFF;
        border-left: 2px solid #D0E4F5;
    }

    table.blueTable thead th:first-child {
        border-left: none;
    }

    table.blueTable tfoot {
        font-size: 14px;
        font-weight: bold;
        color: #FFFFFF;
        background: #D0E4F5;
        background: -moz-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
        background: -webkit-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
        background: linear-gradient(to bottom, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
        border-top: 2px solid #444444;
    }

    table.blueTable tfoot td {
        font-size: 14px;
    }

    table.blueTable tfoot .links {
        text-align: right;
    }

    table.blueTable tfoot .links a {
        display: inline-block;
        background: #1C6EA4;
        color: #FFFFFF;
        padding: 2px 8px;
        border-radius: 5px;
    }

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