<template>
    <v-card class="mx-auto" tile flat>
        <v-toolbar color="primary" dark>
            <v-btn icon @click="$router.push('/Settings')">
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-toolbar-title>System Users</v-toolbar-title>
        </v-toolbar>

        <v-card flat>
            <v-card-text style="height: calc(100vh - 110px); overflow-y: scroll;" class="pa-0">
                <v-list subheader>
                    <v-list-item @click="editSystemUser(user)" v-for="(user, idx) in users" :key="idx">
                        <v-list-item-avatar>
                            <v-icon>mdi-account</v-icon>
                        </v-list-item-avatar>
                        <v-list-item-content>
                            <v-list-item-title>{{ user.firstname + ' ' + user.lastname }}</v-list-item-title>
                            <v-list-item-subtitle>{{ user.uid }}</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>
                </v-list>
            </v-card-text>
        </v-card>

        <v-btn @click="$router.push('/SystemUsers/Create')" color="pink" dark fab style="position: fixed; bottom: 40px; right: 30px;">
            <v-icon>mdi-plus</v-icon>
        </v-btn>
    </v-card>
</template>

<script>
    export default {
        data() {
            return {
                users: [],
            }
        },
        mounted() {
            let self = this;
            self.getSystemUsers();
        },
        methods: {
            getSystemUsers() {
                let self = this;

                self.get('user')
                    .then(r => {
                        self.users = r.data.users ;
                    })
            },
            editSystemUser(user) {
                let self = this;

                self.$router.push('/SystemUsers/Edit/' + user.id);
            }
        }
    }
</script>