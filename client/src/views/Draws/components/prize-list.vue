<template>
    <div>
        <div>Prizes
            <v-btn icon @click="addPrize">
                <v-icon> mdi-plus
                </v-icon>
            </v-btn>
        </div>

        <table style="width: 100%;" class="blueTable">
            <tr style="text-align: left">
                <th>Name</th>
                <th>Value</th>
                <th>Action</th>
            </tr>
            <tr v-if="prizes!=null" v-for="(item,idx) in prizes" :key="idx">
                <td>{{item.description}} </td>
                <td v-if="item.show_value">R {{item.prize_value}}</td>
                <td v-if="item.percentage_of_pot">{{item.pot_percentage}}% of Pot</td>
                <td>
                    <a href="#" @click="editPrize(item)">Edit</a>
                    <v-btn icon @click="deletePrize(item)" color="error">
                        <v-icon>
                            mdi-delete
                        </v-icon>
                    </v-btn>
                </td>
            </tr>
        </table>
    </div>
</template>
<script>
    export default {
        props: ["prizes", "addPrize","editPrize","deletePrize"],
        data() {
            return {

            }
        }
    }
</script>

<style scoped>
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
</style>