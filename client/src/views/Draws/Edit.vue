<template>
    <div>

        <v-container fluid class="fill-height" v-if="draw != null">
            <v-row no-gutters>
                <v-col cols="12">
                    <label>Draw name</label>
                    <v-text-field v-model="draw.draw_name" dense outlined></v-text-field>
                </v-col>
                <v-col cols="12">
                    <label>Draw type</label>
                    <v-select v-model="draw.draw_type" :items="drawTypes" dense outlined></v-select>
                </v-col>
                <v-col cols="12">
                    <label>Issue open</label>
                    <datetime type="datetime" format="yyyy-MM-dd HH:mm:ss" :minute-step="15"
                        :max-datetime="draw.issue_close" v-model="draw.issue_open" class="input">
                    </datetime>
                </v-col>
                <v-col cols="12">
                    <label>Issue close</label>
                    <datetime type="datetime" format="yyyy-MM-dd HH:mm:ss" :minute-step="15"
                        :min-datetime="draw.issue_open" v-model="draw.issue_close" class="input">
                    </datetime>
                </v-col>
                <v-col cols="12">
                    <label>Draw time</label>
                    <datetime type="datetime" format="yyyy-MM-dd HH:mm:ss" :minute-step="15"
                        :min-datetime="draw.issue_close" v-model="draw.draw_time" class="input">
                    </datetime>
                </v-col>
                <v-col cols="12" v-if="draw.draw_type == 2">
                    <label>Ticket cost</label>
                    <v-text-field v-model="draw.ticket_cost" prefix="R" dense outlined></v-text-field>
                </v-col>
            </v-row>
            <input accept="image/*" ref="FileBackGround" @change="uploadBackImage" type="file" style="display: none;">
            <input accept="image/*" ref="fileTitleDialog" @change="uploadtitleImage" type="file" style="display: none;">
            <v-row>
                <v-col cols="12">
                    <PrizeList :editPrize="editPrize" :addPrize="addPrize" :prizes="draw.prizes"
                        :deletePrize="deletePrize" />
                </v-col>
            </v-row>
            <v-row no-gutters>
                <v-col cols="12" xs="12" sm="12" md="12" lg="3" xl="3" class="pa-1" style="text-align: left;">
                    <label>Draw Background <v-btn icon color="error" @click="deleteIMG('DrawTime/draw_background')">
                            <v-icon>mdi-delete
                            </v-icon>
                        </v-btn></label>
                    <v-card height="200" width="355" @click="openFileBackGroundDialog" v-if="showImage">
                        <object height="200" width="100%" :data="getImg('draw_background')" type="image/png">
                            <img height="100%" src="img/no-image.png" style="width:100% !important">
                        </object>
                    </v-card>
                </v-col>
                <v-col cols="12" xs="12" sm="12" md="12" lg="3" xl="3" class="pa-1" style="text-align: left;">
                    <label>Title Image<v-btn icon color="error" @click="deleteIMG('DrawTime/draw_title')">
                            <v-icon>mdi-delete
                            </v-icon>
                        </v-btn> </label>
                    <v-card height="250" width="250" @click="openfileTitleDialog" v-if="showImage">
                        <object height="250" width="100%" :data="getImg('draw_title')" type="image/png">
                            <img height="100%" src="img/no-image.png" style="width:100% !important">
                        </object>
                    </v-card>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12">
                    <v-btn @click="validate" block color="primary" large>Update Draw</v-btn>
                </v-col>
                <!-- <v-col cols="12" class="pt-0">
                    <v-btn block color="primary" large>
                        <div>
                            <div>Entries</div>
                            <div>(239 tickets 82 persons)</div>
                        </div>
                    </v-btn>
                </v-col> -->
                <v-col cols="12" class="pt-0">
                    <v-btn block color="error" large @click="cancelDraw">Cancel Draw</v-btn>
                </v-col>

            </v-row>
            <v-snackbar color="success" v-model="snackbar" :timeout="2000">
                Draw successfully updated
                <template v-slot:action="{ attrs }">
                    <v-btn text v-bind="attrs" @click="snackbar = false">
                        Close
                    </v-btn>
                </template>
            </v-snackbar>
        </v-container>
        <Participants ref="participantsModal" />
        <prizeModal ref="prizeModal" />
        <YesNoModal ref="YesNoModal" />
        <ErrorDialog ref="ErrorDialog" />
        <Cropper ref="Cropper" />
    </div>
</template>

<script>
    import axios from 'axios'
    import PrizeList from './components/prize-list.vue'
    import Participants from './components/participants-modal.vue'
    import prizeModal from './components/prize-modal.vue'
    import YesNoModal from '@/components/YesNoModal.vue'
    import Cropper from "@/components/Cropper.vue"




    import {
        Datetime
    } from 'vue-datetime';

    export default {
        components: {
            Cropper,
            PrizeList,
            Datetime,
            Participants,
            prizeModal,
            YesNoModal,
        },
        data() {
            return {
                snackbar: false,
                showImage: true,
                imageSrc: '',
                progress: 100,
                file: null,
                fileChanged: false,
                issue_close: null,
                issue_open: null,
                draw_time: null,
                draw: null,
                drawTypes: [{
                        text: 'No entry fee',
                        value: 1,
                    },
                    {
                        text: 'Entry fee',
                        value: 2,
                    }
                ]
            }
        },
        mounted() {
            let self = this;
            self.getDraw(cb => {

                // self.draw.issue_close = self.issue_close
                // self.draw.issue_open = self.issue_open
                // self.draw.draw_time = self.draw_time
            });
        },
        methods: {
            checkImage(file, callback) {
                let self = this
                var fr = new FileReader;

                fr.onload = function () { // file is loaded
                    var img = new Image;

                    img.onload = function () {

                        if (img.width != img.height || file.type.toLowerCase() != "image/png") {
                            self.$refs.ErrorDialog.show(
                                "Please ensure prize image is a square (height and width must be the same) and file type is png",
                                res => {})
                        } else {
                            callback()
                        }
                    };

                    img.src = fr.result; // is the data URL because called with readAsDataURL
                };

                fr.readAsDataURL(file)
            },
            deleteIMG(directory) {
                let self = this
                self.showImage = false
                self.$refs.YesNoModal.show("error", "Delete Image", "Are you sure you want to delete this image?",
                    afterRuturn => {
                        if (afterRuturn) {
                            self.showImage = false
                            axios.delete(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'delete?path=' + directory +
                                    '/' + self.draw.id)
                                .then(r => {
                                    console.log(r.data);
                                    if (directory == "DrawTime/draw_background") {
                                        self.draw.bk_image = ""
                                    } else {
                                        self.draw.title_image = ''
                                    }
                                    self.updateDraw(false)
                                    self.showImage = true
                                }).catch(e => {
                                    alert("error deleteing image " + e.toString())
                                    self.showImage = true
                                })
                        } else {
                            self.showImage = true
                        }

                    })

            },
            getImg(directory) {
                let self = this
                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/" + directory + '/' + self.draw.id + ".png"
            },
            uploadtitleImage(e) {
                let self = this;
                let file = e.target.files[0];
                self.showImage = false
                self.$refs.Cropper.show(file, 1, afterComplete => {

                    self.checkImage(afterComplete, cb => {
                        const formData = new FormData();
                        formData.append('file', afterComplete)

                        formData.drawID = self.draw.id
                        const config = {
                            headers: {
                                'content-type': 'multipart/form-data'
                            }
                        }

                        axios.post(process.env.VUE_APP_IMAGE_SERVER_ADDRESS +
                                'upload?path=DrawTime/draw_title/' + self
                                .draw.id, formData, config)
                            .then(r => {
                                self.showImage = true
                                self.draw.title_image = self
                                    .draw.id + '.png'
                                self.updateDraw(false)
                                console.log(r.data);
                            })
                    })
                })
            },
            uploadBackImage(e) {
                let self = this;
                let file = e.target.files[0];
                self.$refs.Cropper.show(file, 1.7777777777, afterComplete => {
                    self.showImage = false
                    const formData = new FormData();
                    formData.append('file', file)

                    formData.drawID = self.draw.id
                    const config = {
                        headers: {
                            'content-type': 'multipart/form-data'
                        }
                    }
                    axios.post(process.env.VUE_APP_IMAGE_SERVER_ADDRESS +
                            'upload?path=DrawTime/draw_background/' + self.draw.id,
                            formData, config)
                        .then(r => {
                            console.log(r.data);
                            self.draw.bk_image = self.draw.id + '.png'
                            self.showImage = true
                            self.updateDraw(false)
                        }).catch(err => {

                        })
                })
            },
            openFileBackGroundDialog() {
                let self = this;
                self.$refs.FileBackGround.value = null;
                self.$refs.FileBackGround.click();
            },
            openfileTitleDialog() {
                let self = this;
                self.$refs.fileTitleDialog.value = null;
                self.$refs.fileTitleDialog.click();
            },
            blobToDataUrl(blob, callback) {
                var a = new FileReader();

                a.onload = function () {
                    callback(a.result);
                }

                a.readAsDataURL(blob);
            },
            validate() {
                let self = this
                let valid = true
                console.log(self.draw);

                if (self.draw.draw_name == "" ||
                    self.draw.issue_open == "" ||
                    self.draw.issue_close == "" ||
                    self.draw.draw_time == "") {
                    self.$refs.ErrorDialog.show("Please ensure all draw details are filled in",
                        val => {})
                } else {
                    if (
                        (self.draw.entry_fee == true && parseFloat(self.draw.ticket_cost) < 1)) {
                        self.$refs.ErrorDialog.show("Ticket cost must be greater than 0",
                            val => {})
                    } else {
                        self.updateDraw(true)
                    }
                }
            },
            cancelDraw() {
                let self = this
                self.$refs.YesNoModal.show("error", "Cancel Draw", "Are you sure you wish to cancel this draw?",
                    value => {
                        if (value) {
                            self.draw.cancelled = true
                            self.put('dt_draw', self.draw)
                                .then(r => {
                                    self.$router.push('/Landing')
                                })
                        }
                    })

            },
            editPrize(item) {
                let self = this
                self.$refs.prizeModal.show(item, newPrize => {
                    let index = self.draw.prizes.indexOf(item)
                    let addablePrize = JSON.parse(JSON.stringify(newPrize))
                    for (var prop in self.draw.prizes[index]) {
                        self.draw.prizes[index][prop] = addablePrize[prop]
                    }
                })
            },
            deletePrize(item) {
                let self = this
                self.$refs.YesNoModal.show("error", "Delete Prize", "Are you sure you wish to delete this prize?",
                    value => {
                        if (value) {
                            self.delete(`dt_draw_prize?id=` + item.id).then(r => {
                                self.draw.prizes.splice(self.draw.prizes.indexOf(item), 1)
                            }).catch(e => {
                                alert("failed to delete prize" + e.toString())
                                console.error(e);
                            })
                        }
                    })
            },
            addPrize() {
                let self = this
                if (self.draw.prizes == null) {
                    self.draw.prizes = []
                }
                if (self.draw.prizes.length == 5) {
                    self.$refs.ErrorDialog.show(
                        "Only five prizes may be created per draw.To create more please delete some existing prizes.",
                        res => {})
                } else {
                    let prize = {
                        id: 0,
                        draw_id: self.draw.id,
                        description: "New prize " + (self.draw.prizes.length + 1),
                        sequence_no: 1,
                        show_value: true,
                        percentage_of_pot: false,
                        pot_percentage: 0,
                        prize_value: 1,
                        prize_cost: 2
                    }
                    self.post('dt_draw_prize', prize).then(r => {
                        console.log("add prize", r.data);

                        self.$refs.prizeModal.show(r.data.dt_draw_prize, newPrize => {
                            let addablePrize = JSON.parse(JSON.stringify(newPrize))
                            self.draw.prizes.push(addablePrize)
                        })
                    })
                }
            },
            getDraw(callback) {
                let self = this;
                let drawID = self.$route.params.draw_id;
                self.get('dt_draw/' + drawID)
                    .then(r => {




                        self.draw = r.data.dt_draw;
                        //  self.draw.issue_open = self.FormatDateTimeGMT(r.data.dt_draw.issue_open)
                        // self.draw.issue_close = self.FormatDateTimeGMT(r.data.dt_draw.issue_close)
                        // self.draw.draw_time = self.FormatDateTimeGMT(r.data.dt_draw.draw_time)


                        console.log('self.issue_open', self.draw.issue_open);
                        console.log('self.issue_open', self.FormatDateTime(self.draw.issue_open));



                        console.log('self.issue_close', self.draw.issue_close);
                        console.log('self.draw_time', self.draw.draw_time);
                        console.log("getDraw", self.draw);

                        callback()
                    })
            },
            updateDraw(reload) {
                let self = this;
                self.draw.issue_open = self.FormatDateTimeGMT(self.draw.issue_open)
                self.draw.issue_close = self.FormatDateTimeGMT(self.draw.issue_close)
                self.draw.draw_time = self.FormatDateTimeGMT(self.draw.draw_time)

                if (self.draw.draw_time < self.draw.issue_close) {
                    self.$refs.ErrorDialog.show(
                        "Please ensure Draw time is after issue close",
                        res => {})

                } else {
                    self.put('dt_draw', self.draw)
                        .then(r => {
                            console.log("update draw", r);
                            // self.snackbar = true
                            if (reload) {
                                self.$router.back()
                            }
                        })
                }
                console.log("self.draw.issue_open", self.draw.issue_open);
                console.log("self.draw.issue_close", self.draw.issue_close);
                console.log("self.draw.draw_time", self.draw.draw_time);

            }
        }
    }
</script>

<style scoped>
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