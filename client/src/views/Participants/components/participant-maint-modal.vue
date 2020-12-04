<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" width="500"  transition="dialog-bottom-transition">
            <v-card >
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
                            <h1>PAID: R{{getTotalDue}}</h1>
                        </v-col>
                        <v-col cols="1" style="text-align: left;" v-if="isAdd">
                            <v-checkbox class="ma-0 mt-0" v-model="accepts">
                            </v-checkbox>
                        </v-col>
                        <v-col cols="11">
                            <a href="#" class="ma-0 mt-2" @click="openTerms"> Participant accept the T's & C's</a>
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
        <ErrorDialog ref="ErrorDialog" />
        <termsModal ref="termsModal" />
    </v-row>
</template>

<script>
    import ErrorDialog from '@/components/Common/Overlays/Dialogs/Error.vue'
    import termsModal from "./terms-modal.vue"

    export default {
        components: {
            termsModal,
            ErrorDialog
        },
        data() {
            return {
                dialog: false,
                participant: null,
                afterReturn: null,
                accepts: false,
                isAdd: true,
                ticket_cost: null
            }
        },
        computed: {
            getTotalDue() {
                let self = this
                if (self.participant != null) {
                    return self.participant.tickets * self.ticket_cost
                }
            }
        },
        methods: {
            openTerms() {
                let self = this
                self.$refs.termsModal.open()

            },
            show(participant, dt_draw, isAdd, afterReturn) {
                let self = this;
                console.log(dt_draw);
                self.accepts=false
                self.ticket_cost = dt_draw.ticket_cost
                self.participant = participant;
                if (self.participant.tickets == null) {
                    self.participant.tickets = 1
                }
                self.isAdd = isAdd;
                self.afterReturn = afterReturn;
                self.dialog = true;
            },
            returnParticipant() {
                let self = this;
                self.validate(val => {
                    console.log("[VALIDATE]", val);

                    if (val) {
                        self.afterReturn(self.participant);
                        self.dialog = false;
                    } else {
                        self.$refs.ErrorDialog.show("Please ensure all participant details are filled in",
                            value => {})
                    }

                })

            },
            validate(callback) {
                let self = this
                let valid = true
                console.log("[PARTICIPANTS]", self.participant);
                if ((self.participant.reference == "" || self.participant.reference == null || self.participant
                        .display_name == "" || self.participant.display_name == null) || self.participant.tickets < 1 ||self.participant.tickets == "") {
                    
                    valid = false
                }
                callback(valid)
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