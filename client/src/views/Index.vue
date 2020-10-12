<template>
    <div>
        <v-toolbar color="primary" dense dark>
            <v-toolbar-title v-if="config!=null">
                Draw Time - {{config.venue_name}}
            </v-toolbar-title>
        </v-toolbar>
        <!-- <router-link to="/Landing">Landing</router-link>
        |
        <router-link to="/CreateDraw">Create Draw</router-link>
        |
        <router-link to="/Participants">Participants</router-link> -->
        <router-view />
    </div>
</template>
<script>
    export default {
        data() {
            return {
                config: null,
            }
        },
        methods: {
            getSystemConfig() {
                let self = this;

                self.get('dt_config')
                    .then(r => {
                        self.config = r.data.dt_config;
                    })
            },
        },
        mounted() {
            this.getSystemConfig()
        },
    }
</script>