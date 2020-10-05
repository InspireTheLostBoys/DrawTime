<template>
    <v-card class="mx-auto" tile flat v-if="config != null">
        <v-toolbar color="primary" dark>
            <!-- <v-app-bar-nav-icon></v-app-bar-nav-icon> -->
            <v-btn icon @click="$router.push('/Landing')">
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-toolbar-title>Settings</v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>

        <v-list subheader three-line>
            <VenueName :config="config" :updateSystemConfig="updateSystemConfig" />
            <v-list-item to="/SystemUsers">
                <v-list-item-content>
                    <v-list-item-title>System Users</v-list-item-title>
                    <v-list-item-subtitle>Set the content filtering level to restrict appts that can be downloaded
                    </v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <!-- <v-divider></v-divider>

        <v-list flat subheader three-line>
            <v-subheader>General</v-subheader>

            <v-list-item-group v-model="settings" multiple active-class="">
                <v-list-item>
                    <template v-slot:default="{ active }">
                        <v-list-item-action>
                            <v-checkbox :input-value="active"></v-checkbox>
                        </v-list-item-action>

                        <v-list-item-content>
                            <v-list-item-title>Notifications</v-list-item-title>
                            <v-list-item-subtitle>Notify me about updates to apps or games that I downloaded
                            </v-list-item-subtitle>
                        </v-list-item-content>
                    </template>
                </v-list-item>

                <v-list-item>
                    <template v-slot:default="{ active }">
                        <v-list-item-action>
                            <v-checkbox :input-value="active"></v-checkbox>
                        </v-list-item-action>

                        <v-list-item-content>
                            <v-list-item-title>Sound</v-list-item-title>
                            <v-list-item-subtitle>Auto-update apps at any time. Data charges may apply
                            </v-list-item-subtitle>
                        </v-list-item-content>
                    </template>
                </v-list-item>

                <v-list-item>
                    <template v-slot:default="{ active }">
                        <v-list-item-action>
                            <v-checkbox :input-value="active"></v-checkbox>
                        </v-list-item-action>

                        <v-list-item-content>
                            <v-list-item-title>Auto-add widgets</v-list-item-title>
                            <v-list-item-subtitle>Automatically add home screen widgets when downloads complete
                            </v-list-item-subtitle>
                        </v-list-item-content>
                    </template>
                </v-list-item>
            </v-list-item-group>
        </v-list> -->
    </v-card>
</template>

<script>
    import VenueName from './VenueName/Index.vue'

    export default {
        components: {
            VenueName
        },
        data() {
            return {
                config: null,
            }
        },
        mounted() {
            let self = this;
            self.getSystemConfig();
        },
        methods: {
            getSystemConfig() {
                let self = this;

                self.get('dt_config')
                    .then(r => {
                        self.config = r.data.dt_config;
                    })
            },
            updateSystemConfig(request, callback) {
                let self = this;

                self.put('dt_config', request)
                    .then(r => {
                        callback(r.data);
                    })
            }
        }
    }
</script>