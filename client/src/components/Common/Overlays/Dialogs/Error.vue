<template>
    <div>
        <v-dialog v-model="dialog" width="400">
            <v-card>
                <v-card-title class="headline pink lighten-2" style="color: white; background: #fa41cc!important;" primary-title>Error</v-card-title>

                <v-card-text class="mt-4">
                    <div class="font-weight-bold" style="font-size: 18px!important;">{{ text }}</div>
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn text @click="dialog = false">
                        Ok
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                dialog: false,
                text: "",
                afterReturn: null
            }
        },
        methods: {
            show(text, afterReturn) {
                let self = this;
                self.text = text;
                self.afterReturn = afterReturn;
                self.dialog = true;
            },
            onAccept() {
                let self = this;
                self.afterReturn();
            }
        }
    }
</script>