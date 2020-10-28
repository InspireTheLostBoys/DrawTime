<template>
    <div>
        <v-container fluid v-if="dt_draw != null">
            <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
            <div class="mt-1" v-if="dt_draw_participants!=null">{{ dt_draw.total_tickets }} tickets
                {{dt_draw_participants.length}} persons</div>
            <h4 class="mt-1">Prize value: R{{dt_draw.total_pot}}</h4>
            <v-btn class="my-3" large block color="primary" @click="editDraw" v-if="userAccess.role_id!=9">Edit Draw
            </v-btn>
            <v-btn class="my-3" large block color="primary" @click="addEntry">Add Entry</v-btn>
            <DuplicateDraw :draw="dt_draw" />

            <v-text-field v-model="searchText" append-icon="mdi-magnify" placeholder="Search..." dense hide-details outlined class="mb-3 pt-2">
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
                        <tr v-for="item in filteredParticipants" :key="item.name">
                            <td>{{ item.reference }}</td>
                            <td>{{ item.display_name }}</td>
                            <td>{{ item.tickets }}</td>
                            <td>
                                <a @click.prevent="editEntry(item)" href="#">Edit</a>
                                <v-btn icon @click="deleteEntry(item)" color="error">
                                    <v-icon>
                                        mdi-delete
                                    </v-icon>
                                </v-btn>
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
                                <img :src="`https://cdn.vuetifyjs.com/images/bottom-sheets/${tile.img}`"
                                    :alt="tile.title">
                            </v-avatar>
                        </v-list-item-avatar>
                        <v-list-item-title>{{ tile.title }}</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-bottom-sheet>
            <ParticipantsMaint ref="participantsMaint" />
            <YesNoModal ref="YesNoModal" />
        </v-container>
    </div>
</template>

<script>
    import ParticipantsMaint from './components/participant-maint-modal.vue'
    import DuplicateDraw from '@/components/Common/DuplicateDraw.vue'
    import YesNoModal from '@/components/YesNoModal.vue'

    export default {
        components: {
            ParticipantsMaint,
            YesNoModal,
            DuplicateDraw
        },
        data() {
            return {
                userAccess: null,
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
        computed: {
            filteredParticipants() {
                let self = this
                if (self.searchText == "") {
                    return self.dt_draw_participants
                } else {
                   return  this.dt_draw_participants.filter((story) => {
                        if (story.display_name.toLowerCase().includes(this.searchText.toLowerCase())||story.reference.toLowerCase().includes(this.searchText.toLowerCase()))
                        {
                            return story
                        }; 
                    })
                }
            }
        },
        
        mounted() {
            let self = this;
            self.getDrawDetails();
            self.getDrawParticipants();
            self.getUserAccess()
        },
        methods: {
            getUserAccess() {
                let self = this
                console.log(localStorage.getItem("userDetails"));

                self.userAccess = JSON.parse(localStorage.getItem("userDetails"));
            },
            deleteEntry(item) {
                let self = this
                self.$refs.YesNoModal.show("error", "Delete Entry", "Are you sure you wish to delete this entry?",
                    value => {
                        if (value) {
                            self.delete('dt_draw_participant?participant_id=' + item.id)
                                .then(r => {
                                    self.dt_draw_participants.splice(self.dt_draw_participants.indexOf(item), 1)
                                })
                        }
                    })
            },
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

                self.$refs.participantsMaint.show(blankParticipant, self.dt_draw, true, participant => {
                    self.post('dt_draw_participant', participant)
                        .then(r => {
                            self.dt_draw_participants.push(r.data.dt_draw_participant);
                        })
                });
            },
            editEntry(entry) {
                let self = this;

                self.$refs.participantsMaint.show(entry, self.dt_draw, false, participant => {
                    self.put('dt_draw_participant', participant)
                        .then(r => {
                            for (var prop in entry) {
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