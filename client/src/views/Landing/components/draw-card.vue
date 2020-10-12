<template>
    <v-card @click="canPlay(draw)">
        <v-card-text>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <h3>{{ draw.draw_name }}</h3>
                        <div>Wed 20 Aug 2020 19:00</div>
                        <div>
                            <v-chip label x-small color="primary">
                                Issue Closed
                            </v-chip>
                            <v-chip class="ml-1" label x-small color="primary">
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
    props: ['draw',"canPlay"]
}
</script>