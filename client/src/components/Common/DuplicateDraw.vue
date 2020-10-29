<template>
    <v-dialog v-model="dialog">
        <template v-slot:activator="{ on, attrs }">
            <v-btn block color="primary" dark v-bind="attrs" v-on="on">
                duplicate draw
            </v-btn>
        </template>
        <v-card>
            <v-toolbar color="primary" dark>
                <v-toolbar-title>
                    Duplicate: {{draw.draw_name}}
                </v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <v-checkbox class="pa-0" v-model="duplicatePrizes" label="Duplicate Prizes"></v-checkbox>
                <v-checkbox class="pa-0 ma-0" v-model="duplicateParticipants" label="Duplicate Participants"></v-checkbox>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn @click="dialog = false" color="error">cancel</v-btn>
                <v-btn @click="submit" color="success">submit</v-btn>

            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    export default {
        props: ["draw"],
        data() {
            return {
                duplicatePrizes: false,
                duplicateParticipants: false,
                dialog: false
            }
        },
        methods: {
            submit() {
                let self = this
                self.draw.draw_name = self.draw.draw_name + " - Duplicate"
                self.post(
                    `dt_draw/duplicate?duplicatePrizes=${self.duplicatePrizes}&duplicateParticipants=${self.duplicateParticipants}`,
                    self.draw).then(r => {
                    self.dialog = false
                    self.$router.push('/Participants/' + r.data.id)
                })
            }
        }
    }
</script>