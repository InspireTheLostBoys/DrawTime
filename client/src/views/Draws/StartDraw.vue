<template>
    <v-container fluid v-if="dt_draw != null">
        <h2 class="mt-2">{{ dt_draw.draw_name }}</h2>
        <div class="mt-1">502 tickets 23 persons</div>
        <h4 class="mt-1">Prize value: R1750</h4>
        <div v-if="dt_draw!=null" v-for="(prize,idx) in dt_draw.prizes" :key="idx">
            <v-btn v-if="prize.show_value" class="my-3" large block color="primary" @click="''">Draw for
                {{prize.description}} - R{{prize.prize_value}}</v-btn>
            <v-btn v-if="prize.percentage_of_pot" class="my-3" large block color="primary" @click="''">Draw for
                {{prize.description}} - {{prize.pot_percentage}}% of Pot</v-btn>
        </div>
        <YesNoModal ref="YesNoModal" />
    </v-container>
</template>
<script>
    import YesNoModal from '@/components/YesNoModal.vue'

    export default {
        components: {
            YesNoModal
        },
        data() {
            return {
                dt_draw: null,
            }
        },
        mounted() {
            let self = this
            self.getDrawDetails();

        },
        methods: {
            getDrawDetails() {
                let self = this;
                let draw_id = self.$route.params.draw_id;
                self.get('dt_draw/' + draw_id)
                    .then(r => {
                        self.dt_draw = r.data.dt_draw;
                    })
            },
        }
    }
</script>