<template>
    <v-card class="mx-auto" tile flat>
        <v-toolbar color="primary" dark>
            <v-btn icon @click="$router.push('/SystemUsers')">
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-toolbar-title>Create System User</v-toolbar-title>
        </v-toolbar>

        <v-card flat>
            <v-card-text>
                <v-row>
                    <v-col cols="12">
                        <label>First name</label>
                        <v-text-field v-model="firstname" outlined dense></v-text-field>
                        <label>Last name</label>
                        <v-text-field v-model="lastname" outlined dense></v-text-field>
                        <label>CMS ID</label>
                        <v-text-field v-model="cms_id" outlined dense></v-text-field>
                        <label>UID</label>
                        <v-text-field v-model="uid" outlined dense></v-text-field>
                        <label>Pin</label>
                        <v-text-field v-model="pin" outlined dense></v-text-field>
                        <v-checkbox v-model="inactive" label="Inactive"></v-checkbox>
                         <label>Role</label>
                        <v-select v-model="role" :items="roles" outlined dense></v-select>
                    </v-col>
                </v-row>
            </v-card-text>
            <v-card-actions class="px-5">
                <v-btn color="grey" @click="$router.push('/SystemUsers')">Cancel</v-btn>
                <v-spacer></v-spacer>
                <v-btn @click="createSystemUser" color="primary">Create</v-btn>
            </v-card-actions>
        </v-card>
    </v-card>
</template>

<script>
    export default {
        data() {
            return {
                firstname: '',
                lastname: '',
                cms_id: '',
                uid: '',
                pwd: '',
                pin: '',
                role:null,
                inactive: false,
                roles: [{
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
                ]

            }
        },
        mounted() {
            let self = this;
        },
        methods: {
            createSystemUser() {
                let self = this;

                let request = {
                    role_id: self.role,
                    user: {
                        firstname: self.firstname,
                        lastname: self.lastname,
                        cms_id: self.cms_id,
                        uid: self.uid,
                        pwd: self.pwd,
                        pin: self.pin,
                        inactive: self.inactive
                    }
                }

                self.post('user', request)
                    .then(r => {
                        self.$router.push('/SystemUsers');
                    })
            }
        }
    }
</script>