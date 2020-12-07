<template>
    <div>
        <v-container fluid class="fill-height">
            <v-row v-if="userAccess!=null">
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3" v-if="userAccess.role_id != 9">
                    <v-btn class="pa-3" large block color="primary" to="/CreateDraw">
                        Create Draw
                    </v-btn>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3" v-if="userAccess.role_id != 9">
                    <v-btn class="pa-3" large block color="info" to="/History">History</v-btn>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3" v-if="userAccess.role_id != 9">
                    <v-btn class="pa-3" large block color="info" to="/Adverts">Adverts</v-btn>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3" v-if="userAccess.role_id == 1">
                    <v-btn class="pa-3" large block color="info" to="/Settings">Settings</v-btn>
                </v-col>
                <v-col cols="6" xs="6" sm="6" md="3" lg="3" xl="3" v-if="userAccess.role_id == 1">
                    <v-btn class="pa-3" large block color="info" to="/Reports">reports</v-btn>
                </v-col>
            </v-row>
            <v-row>
                <v-img contain max-height="500" style="overflow-y:auto" src="img/DrawTime-Logo.png">
                    <v-row  >
                        <v-col cols="12" xs="12" sm="12" md="6" lg="6" xl="6" v-for="(draw, idx) in draws" :key="idx">
                            <Drawcard :canPlay="canPlay" :draw="draw" />
                        </v-col>
                    </v-row>
                </v-img>
            </v-row>
            <ErrorDialog ref="ErrorDialog" />
        </v-container>
    </div>
</template>

<script>
    import Drawcard from './components/draw-card.vue';
    import ErrorDialog from '@/components/Common/Overlays/Dialogs/Error.vue'


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
                        } else {
                            self.$refs.ErrorDialog.show("You do not have permissions to edit an active draw",
                                value => {})
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
                        self.draws = r.data;
                    })
            },
            getUserAccess() {
                let self = this
                self.userAccess = JSON.parse(localStorage.getItem("userDetails"));
            }
        }
    }
</script>