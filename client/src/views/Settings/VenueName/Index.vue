<template>
    <div>
        <v-list-item @click="editVenueName">
            <v-list-item-content>
                <v-list-item-title>Venu Name</v-list-item-title>
                <v-list-item-subtitle style="color: red;" v-if="config.venue_name == ''">Please supply a venue name
                </v-list-item-subtitle>
                <v-list-item-subtitle v-else>{{ config.venue_name }}</v-list-item-subtitle>
            </v-list-item-content>
        </v-list-item>
        <MaintModal ref="maintModal" />
    </div>
</template>

<script>
    import MaintModal from './MaintModal.vue';

    export default {
        props: ['config', 'updateSystemConfig'],
        components: {
            MaintModal
        },  
        methods: {
            editVenueName() {
                let self = this;

                self.$refs.maintModal.show(self.config.venue_name, newName => {
                    let request = {
                        id: self.config.id,
                        venue_name: newName,
                        t_and_c: self.config.t_and_c
                    }

                    self.updateSystemConfig(request, response => {
                        self.config.venue_name = newName;
                    })
                })
            }
        },
    }
</script>