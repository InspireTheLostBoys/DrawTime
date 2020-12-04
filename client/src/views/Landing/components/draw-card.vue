<template>
    <v-card @click="canPlay(draw)">
        <v-card-text>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <h3>{{ draw.draw_name }}</h3>
                        <div>{{FormatDateTime(draw.draw_time)}}</div>
                        <div>
                             <v-chip v-if="checkDrawDate(draw.issue_open)&&!checkDrawDate(draw.issue_close)" label x-small color="primary">
                                Issue open
                            </v-chip>
                            <v-chip v-if="checkDrawDate(draw.issue_close)" label x-small color="primary">
                                Issue Closed
                            </v-chip>
                            <v-chip v-if="checkDrawDate(draw.draw_time)" class="ml-1" label x-small color="primary">
                                Draw Ready
                            </v-chip>
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
</template>

<script>
    export default {
        props: ['draw', "canPlay"],
        methods: {
            checkDrawDate(date) {
                let self = this
                let todayDate = new Date()
                console.log(self.FormatDateTime(date) +' > ' + self.FormatDateTime(todayDate));
                
                console.log(self.FormatDateTime(date) > self.FormatDateTime(todayDate));
                
                
                if (self.FormatDateTime(date) < self.FormatDateTime(todayDate)) {
                    return true
                } else {
                    return false
                }
            }
        }
    }
</script>