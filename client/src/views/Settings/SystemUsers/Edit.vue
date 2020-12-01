<template>
    <v-card class="mx-auto" tile flat>
        <v-toolbar color="primary" dark>
            <v-btn icon @click="$router.push('/SystemUsers')">
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-toolbar-title>Edit System User</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-menu offset-y>
                <template v-slot:activator="{ on, attrs }">
                    <v-btn icon v-bind="attrs" v-on="on">
                        <v-icon>mdi-dots-vertical</v-icon>
                    </v-btn>
                </template>
                <v-list dense class="pa-0">
                    <v-list-item @click="deleteSystemUser">
                        <v-list-item-title>Delete</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-toolbar>

        <v-card flat v-if="user != null">
            <v-card-text>
                <v-row>
                    <v-col cols="12">
                        <label>First name</label>
                        <v-text-field v-model="user.firstname" outlined dense></v-text-field>
                        <label>Last name</label>
                        <v-text-field v-model="user.lastname" outlined dense></v-text-field>
                        <label>CMS ID</label>
                        <v-text-field v-model="user.cms_id" outlined dense></v-text-field>
                        <label>UID</label>
                        <v-text-field v-model="user.uid" outlined dense></v-text-field>
                        <label>Pin</label>
                        <v-text-field v-model="user.pin" outlined dense></v-text-field>
                        <v-checkbox v-model="user.inactive" label="Inactive"></v-checkbox>
                        <label>Role</label>
                        <v-select v-model="role" :items="roles" outlined dense></v-select>
                    </v-col>
                </v-row>
            </v-card-text>
            <v-card-actions class="px-5">
                <v-btn color="grey" @click="$router.push('/SystemUsers')">Cancel</v-btn>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="updateSystemUser">Save</v-btn>
            </v-card-actions>
        </v-card>
    </v-card>
</template>

<script>
    export default {
        data() {
            return {
                user: null,
                roles: [
                    {
                        text: "Administrator",
                        value: 1
                    },
                    {
                        text: "Game Manager",
                        value: 8
                    }, {
                        text: "Participiant Manager",
                        value: 9
                    }
                ],
                role: null,
            }
        },
        mounted() {
            let self = this;
            self.getSystemUser();
        },
        methods: {
            getSystemUser() {
                let self = this;

                let user_id = self.$route.params.user_id;

                self.get('user/' + user_id)
                    .then(r => {
                        self.user = r.data.user;
                        self.role = r.data.role_id
                    })
            },
            updateSystemUser() {
                let self = this;

                let request = {
                    role_id: self.role,
                    user: self.user
                }

                self.put('user', request)
                    .then(r => {
                        self.$router.push('/SystemUsers');
                    })
            },
            deleteSystemUser() {
                let self = this;

                self.delete('user?systemUserID=' + self.user.id)
                    .then(r => {
                        self.$router.push('/SystemUsers');
                    })
            }
        }
    }
</script>