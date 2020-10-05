<template>
    <div>
        <v-container fluid class="fill-height">
            <v-row>
                <v-col cols="12" v-for="(draw, idx) in draws" :key="idx">
                    <Drawcard :draw="draw" />
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12" style="text-align: center;">
                    <v-btn color="primary" width="200" to="/CreateDraw">Create Draw</v-btn>
                </v-col>
                <v-col cols="12" style="text-align: center;">
                    <v-btn color="info" width="200">History</v-btn>
                </v-col>
                <v-col cols="12" style="text-align: center;">
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
                draws: []
            }
        },
        mounted() {
            let self = this;
            self.getDraws();
        },
        methods: {
            getDraws() {
                let self = this;
                
                self.get('dt_draw')
                    .then(r => {
                        self.draws = r.data;
                    })
            },

        }
    }
</script>