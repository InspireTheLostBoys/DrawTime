<template>
    <div>
        <v-toolbar color="primary" dense dark>
            <v-toolbar-title v-if="config!=null" @click="routelanding">
                Draw Time - {{config.venue_name}}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="logout">
                <v-icon>
                    mdi-logout
                </v-icon>
            </v-btn>
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
        components: {

        },
        data() {
            return {
                config: null,
            }
        },
        methods: {
            routelanding() {
                let self = this
                if (self.$route.path != "/Landing")
                    self.$router.push('/Landing')

            },
            getSystemConfig() {
                let self = this;
                self.get('dt_config')
                    .then(r => {
                        self.config = r.data.dt_config;
                    })
            },
            logout() {
                let self = this
                localStorage.clear()
                this.$router.push('/Login')
            },
        },
        mounted() {
            if (localStorage.userDetails == undefined || localStorage.userDetails == null) {
                this.$router.push('/Login')
            } else {
                this.$router.push('/Landing')
            }
            this.getSystemConfig()
        },
    }
</script>