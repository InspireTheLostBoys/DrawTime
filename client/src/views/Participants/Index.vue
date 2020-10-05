<template>
    <v-container fluid v-if="dt_draw != null">
        <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
        <div class="mt-1">502 tickets 23 persons</div>
        <h4 class="mt-1">Prize value: R1750</h4>
        <v-btn class="my-3" large block color="primary" @click="editDraw">Edit Draw</v-btn>
        <v-btn class="my-3" large block color="primary" @click="addEntry">Add Entry</v-btn>
        <v-text-field append-icon="mdi-magnify" placeholder="Search..." dense hide-details outlined class="mb-3">
        </v-text-field>
        <v-simple-table dense fixed-header height="calc(100vh - 240px)">
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
                    <tr v-for="item in dt_draw_participants" :key="item.name">
                        <td>{{ item.reference }}</td>
                        <td>{{ item.display_name }}</td>
                        <td>{{ item.tickets }}</td>
                        <td>
                            <a @click.prevent="editEntry(item)" href="#">Edit</a>
                        </td>
                    </tr>
                </tbody>
            </template>
        </v-simple-table>
        <v-bottom-sheet v-model="sheet">
            <v-list>
                <v-subheader>Open in</v-subheader>
                <v-list-item v-for="tile in tiles" :key="tile.title" @click="sheet = false">
                    <v-list-item-avatar>
                        <v-avatar size="32px" tile>
                            <img :src="`https://cdn.vuetifyjs.com/images/bottom-sheets/${tile.img}`" :alt="tile.title">
                        </v-avatar>
                    </v-list-item-avatar>
                    <v-list-item-title>{{ tile.title }}</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-bottom-sheet>
        <ParticipantsMaint ref="participantsMaint" />
    </v-container>
</template>

<script>
    import ParticipantsMaint from './components/participant-maint-modal.vue'

    export default {
        components: {
            ParticipantsMaint
        },
        data() {
            return {
                dt_draw: null,
                dt_draw_participants: null,
                sheet: false,
                tiles: [{
                        img: 'keep.png',
                        title: 'Keep'
                    },
                    {
                        img: 'inbox.png',
                        title: 'Inbox'
                    },
                    {
                        img: 'hangouts.png',
                        title: 'Hangouts'
                    },
                    {
                        img: 'messenger.png',
                        title: 'Messenger'
                    },
                    {
                        img: 'google.png',
                        title: 'Google+'
                    },
                ],
                searchText: '',
                dialog: false,
                notifications: false,
                sound: true,
                widgets: false
            }
        },
        mounted() {
            let self = this;
            self.getDrawDetails();
            self.getDrawParticipants();
        },
        methods: {
            show() {
                let self = this;
                self.dialog = true;
            },
            getDrawDetails() {
                let self = this;

                let draw_id = self.$route.params.draw_id;

                self.get('dt_draw/' + draw_id)
                    .then(r => {
                        self.dt_draw = r.data.dt_draw;
                    })
            },
            getDrawParticipants() {
                let self = this;

                let draw_id = self.$route.params.draw_id;

                self.get('dt_draw_participant/' + draw_id)
                    .then(r => {
                        self.dt_draw_participants = r.data.dt_draw_participants;
                    })
            },
            addEntry() {
                let self = this;

                let draw_id = self.$route.params.draw_id;

                let blankParticipant = {
                    id: 0,
                    draw_id: draw_id,
                    reference: null,
                    display_name: null,
                    tickets: null,
                    cancelled: false,
                    issued_by: 1,
                }

                self.$refs.participantsMaint.show(blankParticipant, true, participant => {
                    self.post('dt_draw_participant', participant)
                        .then(r => {
                            self.dt_draw_participants.push(r.data.dt_draw_participant);
                        })
                });
            },
            editEntry(entry) {
                let self = this;

                self.$refs.participantsMaint.show(entry, false, participant => {
                    self.put('dt_draw_participant', participant)
                        .then(r => {
                            for(var prop in entry) {
                                entry[prop] = participant[prop];
                            }
                        })
                })
            },
            editDraw() {
                let self = this;

                let draw_id = self.$route.params.draw_id;

                self.$router.push('/EditDraw/' + draw_id);
            }
        }
    }
</script>