<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
            <v-card tile>
                <v-toolbar dark color="primary" flat>
                    <v-btn icon dark @click="dialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-btn dark text @click="dialog = false">
                            Save
                        </v-btn>
                    </v-toolbar-items>
                </v-toolbar>
                <v-card-text class="px-3">
                    <h2 class="mt-2">9 pm promo draw</h2>
                    <div class="mt-2">502 tickets 23 persons</div>
                    <h4 class="mt-1">Prize value: R1750</h4>
                    <v-btn class="my-3" large block color="primary" @click="addEntry">Add Entry</v-btn>
                    <v-text-field append-icon="mdi-magnify" placeholder="Search..." dense hide-details outlined
                        class="mb-3"></v-text-field>
                    <v-simple-table dense fixed-header height="calc(100vh - 285px)">
                        <template v-slot:default>
                            <thead>
                                <tr>
                                    <th class="text-left">
                                        Reference
                                    </th>
                                    <th class="text-left">
                                        Display
                                    </th>
                                    <th class="text-left">
                                        Tickets
                                    </th>
                                    <th class="text-left">
                                        Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in desserts" :key="item.name">
                                    <td>{{ item.reference }}</td>
                                    <td>{{ item.display }}</td>
                                    <td>{{ item.tickets }}</td>
                                    <td>
                                        <a href="#">Edit</a>
                                    </td>
                                </tr>
                            </tbody>
                        </template>
                    </v-simple-table>
                </v-card-text>
            </v-card>
        </v-dialog>
        <!-- <ParticipantsMaint ref="participantsMaint" /> -->
    </v-row>
</template>

<script>
    // import ParticipantsMaint from './participant-maint-modal.vue'

    export default {
        components: {
            // ParticipantsMaint
        },
        data() {
            return {
                searchText: '',
                dialog: false,
                notifications: false,
                sound: true,
                widgets: false,
                desserts: [{
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                    {
                        reference: '0824459616',
                        display: 'Paul',
                        tickets: 159,
                    },
                ],
            }
        },
        methods: {
            show() {
                let self = this;
                self.dialog = true;
            },
            addEntry() {
                let self = this;

                self.$refs.participantsMaint.show();
            }
        }
    }
</script>