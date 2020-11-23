<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" persistent transition="dialog-bottom-transition">
            <v-card tile>
                <v-toolbar dark color="primary" flat>
                    <v-btn icon dark @click="dialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-btn dark text @click="validate()">
                            Save
                        </v-btn>
                    </v-toolbar-items>
                </v-toolbar>
                <v-container fluid class="fill-height">
                    <v-row no-gutters>
                        <v-col cols="12">
                            <label>Description</label>
                            <v-text-field v-model="prize.description" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-checkbox @change="changeType(0)" v-model="prize.show_value" dense outlined
                                label="Show Value"></v-checkbox>
                        </v-col>
                        <v-col cols="12" v-if="prize.show_value">
                            <label>Value</label>
                            <v-text-field v-model="prize.prize_value" dense outlined type="number"></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-checkbox @change="changeType(1)" v-model="prize.percentage_of_pot" dense outlined
                                label="Use Percentage of Pot">
                            </v-checkbox>
                        </v-col>
                        <v-col cols="12" v-if="prize.percentage_of_pot">
                            <label>Percentage</label>
                            <v-text-field v-model="prize.pot_percentage" dense outlined type="number"></v-text-field>
                        </v-col>
                        <v-col cols="12" style="text-align: left;">
                            <label>Prize Image <v-btn icon color="error" @click="deleteIMG">
                                    <v-icon>mdi-delete
                                    </v-icon>
                                </v-btn></label>
                            <v-card height="250" width="250" @click="openFileBackGroundDialog">
                                <object v-if="showImage" height="250" width="250" :data="getImg()" type="image/png">
                                    <img height="100%" src="img/no-image.png" style="width:100% !important">
                                </object>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card>
            <input accept="image/*" ref="FileBackGround" @change="uploadPrizeImage" type="file" style="display: none;">
            <ErrorDialog ref="ErrorDialog" />
            <Cropper ref="Cropper" />
        </v-dialog>
    </v-row>
</template>

<script>
    import axios from 'axios'
    import Cropper from "@/components/Cropper.vue"

    export default {
        components: {
            Cropper
        },
        data() {
            return {
                dialog: false,
                prize: {
                    id: null,
                    draw_id: null,
                    description: null,
                    sequence_no: null,
                    show_value: false,
                    percentage_of_pot: false,
                    pot_percentage: null,
                    prize_value: null,
                    prize_image: "",
                },
                callback: null,
                imageSrc: 'img/no-image.png',
                showImage: true
            }
        },
        methods: {
            deleteIMG() {
                let self = this
                self.showImage = false
                axios.delete(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + '/delete?path=DrawTime/Prizes/' + self.prize.id)
                    .then(r => {
                        self.prize.prize_image = ''
                        self.put(`dt_draw_prize`, self.prize).then(er => {})
                        self.showImage = true
                    })
            },
            openFileBackGroundDialog() {
                let self = this;
                self.$refs.FileBackGround.value = null;
                self.$refs.FileBackGround.click();
            },
            getImg() {
                let self = this

                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/Prizes/" + self.prize.id + ".png"
            },
            checkImage(file, callback) {
                let self = this
                var fr = new FileReader;

                fr.onload = function () { // file is loaded
                    var img = new Image;

                    img.onload = function () {
                        if (img.width != img.height) {
                            self.$refs.ErrorDialog.show(
                                "Please ensure prize image is a square (height and width must be the same)",
                                res => {})
                        } else {
                            callback()
                        }
                    };

                    img.src = fr.result; // is the data URL because called with readAsDataURL
                };

                fr.readAsDataURL(file)
            },
            uploadPrizeImage(e) {
                let self = this;
                let file = e.target.files[0];
                self.$refs.Cropper.show(file, 1, afterComplete => {
                    self.showImage = false
                    self.checkImage(afterComplete, cb => {
                        const formData = new FormData();
                        formData.append('file', afterComplete)
                        const config = {
                            headers: {
                                'content-type': 'multipart/form-data'
                            }
                        }

                        axios.post(process.env.VUE_APP_IMAGE_SERVER_ADDRESS +
                                'upload?path=DrawTime/Prizes/' + self.prize.id, formData,
                                config)
                            .then(r => {
                                self.showImage = true
                                self.prize.prize_image = 'Prizes/' + self.prize.id
                                self.put(`dt_draw_prize`, self.prize).then(er => {
                                    console.log("updated prize", er);
                                })
                            }).catch(err => {

                            })
                    })
                })
            },
            changeType(type) {
                let self = this
                if (type == 0) {
                    self.prize.percentage_of_pot = !self.prize.show_value
                    self.prize.pot_percentage = 0
                } else {
                    self.prize.show_value = !self.prize.percentage_of_pot
                    self.prize.prize_value = 0
                }
            },
            show(prize, callback) {
                let self = this;
                for (var prop in prize) {
                    self.prize[prop] = prize[prop]
                }
                self.callback = callback
                self.dialog = true;
            },
            validate() {
                let self = this
                if ((self.prize.percentage_of_pot && self.prize.pot_percentage <= 0) || (self.prize.show_value && self
                        .prize.prize_value <= 0)) {
                    self.$refs.ErrorDialog.show("Please ensure all prize information is filled in",
                        afterReturn => {})
                } else {
                    self.submit()
                }
            },
            submit() {
                let self = this;
                self.dialog = false
                self.put(`dt_draw_prize`, self.prize).then(er => {
                    console.log("updated prize", er);
                    self.callback(self.prize)

                })
            }
        }
    }
</script>

<style>
    .input {
        height: 40px;
        width: 100%;
        border: 1px solid #9e9e9e;
        border-radius: 5px;
        padding-left: 10px;
        padding-top: 7px;
        margin-bottom: 20px;
    }
</style>