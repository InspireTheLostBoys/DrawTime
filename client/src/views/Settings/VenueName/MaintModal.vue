<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" transition="dialog-bottom-transition">
            <v-card>
                <v-card-text class="px-3 py-3">
                    <v-row no-gutters>
                        <v-col cols="12">
                            <label>Venue Name</label>
                            <v-text-field v-model="venuName" outlined dense>
                            </v-text-field>
                        </v-col>
                    </v-row>
                </v-card-text>
                <v-card-actions>
                    <v-btn @click="dialog = false" text>Cancel</v-btn>
                    <v-spacer></v-spacer>
                    <v-btn @click="returnVenueName" color="primary">Submit</v-btn>
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
                afterReturn: null,
                venuName: "",
            }
        },
        methods: {
            show(venuName, afterReturn) {
                let self = this;
                self.venuName = venuName;
                self.afterReturn = afterReturn;
                self.dialog = true;
            },
            returnVenueName() {
                let self = this;
                self.afterReturn(self.venuName);
                self.dialog = false;
            }
        }
    }
</script>