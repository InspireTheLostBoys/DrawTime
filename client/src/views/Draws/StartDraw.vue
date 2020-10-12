<template>
    <div>

        <v-container fluid v-if="dt_draw != null">
          <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
            <div class="mt-1">{{ dt_draw.total_tickets }} tickets {{dt_draw_participants.length}} persons</div>
            <h4 class="mt-1">Prize value: R{{dt_draw.total_pot}}</h4>
            <div v-if="dt_draw!=null" v-for="(prize,idx) in dt_draw.prizes" :key="idx">
                <v-btn v-if="prize.show_value" class="my-3" large block color="primary" @click="drawPrize(item)">Draw for
                    {{prize.description}} - R{{prize.prize_value}}</v-btn>
                <v-btn v-if="prize.percentage_of_pot" class="my-3" large block color="primary" @click="drawPrize(item)">Draw for
                    {{prize.description}} - {{prize.pot_percentage}}% of Pot</v-btn>
            </div>
            <YesNoModal ref="YesNoModal" />
        </v-container>
    </div>
</template>
<script>
    import YesNoModal from '@/components/YesNoModal.vue'
    import websocketClient from '@/libs/websocket-client'
    export default {
        components: {
            YesNoModal
        },
        data() {
            return {
                dt_draw: null,
                socket: null,
                dt_draw_participants:null,
            }
        },
        mounted() {
            let self = this
            self.getDrawDetails();
            self.initWebsocket()

        },
        methods: {
             getDrawParticipants() {
                let self = this;

                let draw_id = self.$route.params.draw_id;

                self.get('dt_draw_participant/' + draw_id)
                    .then(r => {
                        self.dt_draw_participants = r.data.dt_draw_participants;
                    })
            },
            getDrawDetails() {
                let self = this;
                let draw_id = self.$route.params.draw_id;
                self.get('dt_draw/' + draw_id)
                    .then(r => {
                        console.log(r);
                        
                        self.dt_draw = r.data.dt_draw;
                       self.getDrawParticipants()
                    })
            },
            logError(exc) {
                let self = this
                // console.log(exc);

            },
            onConnect() {
                let self = this
                console.log("connected");

            },
            onMessage(data) {
                let self = this
                console.log(data);

            },
            initWebsocket() {
                let self = this
                let params = {
                    wsAddress: 'ws://192.168.1.177:17501',
                    onConnect: self.onConnect,
                    onMessage: self.onMessage,
                    onError: self.logError
                }
                self.socket = new websocketClient(params);
            },
            drawPrize(item) {
                let self = this

                let DrawMsg = {
                    "MsgType": "STAGEGAMES",
                    "GameType": "DRAWTIME",
                    "Function": "DRAWPRIZE",
                    "Language": "DE",
                    "Translate": ["Welcome", "Mobile Number"],
                    "Draw_ID": self.dt_draw.id,
                    "Draw_Prize_ID": item.id
                }
                self.socket.sendMessage(DrawMsg, self.onMessage)

                // self.wsAddress = params.wsAddress;
                // self.onConnect = params.onConnect;
                // self.onMessage = params.onMessage;
                // self.onError = params.onError;

            },
        }
    }
</script>