<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" transition="dialog-bottom-transition">
            <v-card>
                <v-card-text class="px-3 py-3" v-if="participant != null">
                    <v-row no-gutters>
                        <v-col cols="12">
                            <label>Reference</label>
                            <v-text-field @change="onFieldChange" v-model="participant.reference" outlined dense>
                            </v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Display Name</label>
                            <v-text-field v-model="participant.display_name" outlined dense></v-text-field>
                        </v-col>
                        <v-col cols="6">
                            <label>Tickets</label>
                            <v-text-field v-model="participant.tickets" type="number" outlined dense></v-text-field>
                        </v-col>
                        <v-col cols="6" style="text-align: right;">
                            <div style="height: 30px"></div>
                            <h1>DUE: R0</h1>
                        </v-col>
                        <v-col cols="12" style="text-align: right;" v-if="isAdd">
                            <v-checkbox v-model="accepts" class="ma-0 mt-3" label="Participant accept the T's & C's">
                            </v-checkbox>
                        </v-col>
                    </v-row>
                </v-card-text>
                <v-card-actions>
                    <v-btn @click="dialog = false" text>Cancel</v-btn>
                    <v-spacer></v-spacer>
                    <v-btn :disabled="!accepts && isAdd" @click="returnParticipant" color="primary">Submit</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
    export default {
        data() {
            return {
                dialog: false,
                participant: null,
                afterReturn: null,
                accepts: false,
                isAdd: true
            }
        },
        methods: {
            show(participant, isAdd, afterReturn) {
                let self = this;
                self.participant = participant;
                self.isAdd = isAdd;
                self.afterReturn = afterReturn;
                self.dialog = true;
            },
            returnParticipant() {
                let self = this;
                self.afterReturn(self.participant);
                self.dialog = false;
            },
            onFieldChange() {
                let self = this;

                self.$nextTick(() => {
                    if (self.participant.display_name == null || self.participant.display_name == "") {
                        self.participant.display_name = self.participant.reference;
                    }
                })
            }
        }
    }
</script>