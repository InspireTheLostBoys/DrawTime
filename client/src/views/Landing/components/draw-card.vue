<template>
    <v-card @click="$router.push('/Participants/' + draw.id)">
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
                        <div>239 tickets</div>
                        <div>Tickets cost: R{{ draw.ticket_cost }}</div>
                        <div>
                            <b>Pot: R896</b>
                        </div>
                    </td>
                </tr>
            </table>
        </v-card-text>
    </v-card>
</template>

<script>
export default {
    props: ['draw']
}
</script>