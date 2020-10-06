<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" persistent transition="dialog-bottom-transition">
            <v-card tile>
                <v-toolbar dark color="primary" flat>
                    <v-btn icon dark @click="dialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-btn dark text @click="submit()">
                            Save
                        </v-btn>
                    </v-toolbar-items>
                </v-toolbar>
                <v-container fluid class="fill-height">
                    <v-row no-gutters>
                        <v-col cols="12">
                            <label>Description</label>
                            <v-text-field v-model="prize.description" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-checkbox @change="changeType(0)" v-model="prize.show_value" dense outlined
                                label="Show Value"></v-checkbox>
                        </v-col>
                        <v-col cols="12" v-if="prize.show_value">
                            <label>Value</label>
                            <v-text-field v-model="prize.prize_value" dense outlined type="number"></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-checkbox @change="changeType(1)" v-model="prize.percentage_of_pot" dense outlined
                                label="Use Percentage of Pot">
                            </v-checkbox>
                        </v-col>
                        <v-col cols="12" v-if="prize.percentage_of_pot">
                            <label>Percentage</label>
                            <v-text-field v-model="prize.pot_percentage" dense outlined type="number"></v-text-field>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
    export default {
        components: {},
        data() {
            return {
                dialog: false,
                prize: {
                    id: null,
                    draw_id: null,
                    description: null,
                    sequence_no: null,
                    show_value: false,
                    percentage_of_pot: false,
                    pot_percentage: null,
                    prize_value: null,
                },
                callback: null,
            }
        },
        methods: {
            changeType(type) {
                let self = this
                if (type==0) {
                    self.prize.percentage_of_pot = !self.prize.show_value
                    self.prize.pot_percentage = 0
                }else{
                    self.prize.show_value = !self.prize.percentage_of_pot
                    self.prize.prize_value = 0
                }
            },
            show(prize, callback) {
                let self = this;
                for (var prop in prize) {
                    self.prize[prop] = prize[prop]
                }
                self.callback = callback
                self.dialog = true;
            },
            submit() {
                let self = this;
                self.dialog = false
                self.callback(self.prize)
            }
        }
    }
</script>

<style>
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