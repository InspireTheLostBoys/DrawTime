<template>
    <div>
        <v-toolbar color="primary" dense dark>
            <v-btn icon @click="routelanding()">
                <v-icon>
                    mdi-home
                </v-icon>
            </v-btn>
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
        <v-btn @click="$router.back()" color="primary" class="fab-button" elevation="8" fab>
            <v-icon>
                mdi-arrow-left
            </v-icon>
        </v-btn>
        <YesNoModal ref="YesNoModal" />
    </div>
</template>
<script>
    import YesNoModal from '@/components/YesNoModal.vue'

    export default {
        components: {
            YesNoModal
        },
        data() {
            return {
                config: null,
            }
        },
        methods: {
            routelanding() {
                let self = this
                if (!self.$route.path.includes('Landing'))
                    self.$router.push('/Landing')
            },
            getSystemConfig() {
                let self = this;
                self.get('dt_config')
                    .then(r => {
                        console.log(r.data.dt_config);
                        
                        self.config = r.data.dt_config;
                    }).catch(e=>{
                        console.log(e);
                        
                    })
            },
            logout() {
                let self = this
                self.$refs.YesNoModal.show("warning", "Logout", "Are you sure you wish to Logout?", value => {
                    if (value) {
                        localStorage.clear()
                        this.$router.push('/Login')
                    }
                })
            },
        },
        mounted() {
            if (localStorage.userDetails == undefined || localStorage.userDetails == null) {
                this.$router.push('/Login')
            } else {
                if (!this.$route.path.includes('Landing'))
                    this.$router.push('/Landing')
            }
            this.getSystemConfig()
        },
    }
</script>


<style scoped>
    .fab-button {
        bottom: 5px;
        margin-left: 5px;
        position: fixed;
    }
</style>