<template>
    <div>
       
        <v-container fluid class="fill-height">
            <v-row>
                <v-col cols="12" v-for="(draw, idx) in draws" :key="idx">
                    <Drawcard :canPlay="canPlay" :draw="draw" />
                </v-col>
            </v-row>
            <v-row v-if="userAccess!=null">
                <v-col cols="12" style="text-align: center;">
                    <v-btn color="primary" width="200" to="/CreateDraw" v-if="userAccess.role_id != 9">Create Draw
                    </v-btn>
                </v-col>
                <v-col cols="12" style="text-align: center;">
                    <v-btn color="info" width="200" to="/History">History</v-btn>
                </v-col>
                <v-col cols="12" style="text-align: center;" v-if="userAccess.role_id == 1">
                    <v-btn color="info" width="200" to="/Settings">Settings</v-btn>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
    import Drawcard from './components/draw-card.vue';

    export default {
        components: {
            Drawcard
        },
        data() {
            return {
                draws: [],
                userAccess: null,
            }
        },
        mounted() {
            let self = this;
            self.getDraws();
            self.getUserAccess()
        },
        methods: {
            canPlay(draw) {
                let self = this
                self.get(`dt_draw/CanPlay?draw_id=${draw.id}`).then(r => {
                    if (r.data) {
                        if (self.userAccess.role_id != 9) {
                            self.$router.push('/DrawStart/' + draw.id)
                        }
                    } else {
                        self.$router.push('/Participants/' + draw.id)
                    }
                })
            },
            getDraws() {
                let self = this;

                self.get('dt_draw')
                    .then(r => {
                        console.log("draws",r.data);
                        
                        self.draws = r.data;
                    })
            },
            getUserAccess() {
                let self = this
                console.log(localStorage.getItem("userDetails"));

                self.userAccess = JSON.parse(localStorage.getItem("userDetails"));
            }
        }
    }
</script>