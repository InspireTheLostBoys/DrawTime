<template>
    <v-row justify="center">

        <v-dialog v-model="dialog" fullscreen persistent transition="dialog-bottom-transition">
            <v-toolbar dark color="primary" flat>
                <v-btn icon dark @click="dialog = false">
                    <v-icon>mdi-close</v-icon>
                </v-btn>
                <v-spacer></v-spacer>
                <v-toolbar-title>
                    T's & C's
                </v-toolbar-title>
            </v-toolbar>
            <v-card tile class="fill-height">
                <v-container class="fill-height" style="overflow-y:scroll" fluid>
                    <h3>RULES, TERMS AND CONDITIONS GENERAL</h3>
                    <br>
                    <p>
                        Bonus In A Box (hereafter referred to as “the Product”) is a product developed by Dev Info
                        Technology (Pty) Ltd. (hereafter referred to as “Dev I.T.”) and is licenced for use to the owner
                        of
                        {NAME OF BUSINESS} (Hereafter referred to as “the business”).
                    </p>
                    <p>
                        Dev I.T. accepts no responsibility for any loss or damage, whether physical, mental, financial,
                        or
                        otherwise, incurred through the direct or indirect use of this product.
                    </p>
                    <p>
                        Dev I,T, further accepts no responsibility for any illegal use of this product by the business
                        or
                        any other party.
                    </p>
                    INTRODUCTION
                    <ul>
                        <li>
                            The business is hosting Games which comprises of a variety of games and promotional Leader
                            Board competitions, hereinafter collectively referred to as BIAB.
                        </li>
                        <li>
                            Participation in BIAB games are subject to and governed by the Rules and the Terms as
                            specified below.
                        </li>
                        <li>
                            Participants are to review and familiarize themselves with the Rules and the Terms before
                            entering BIAB games.
                        </li>
                        <li>By participating in BIAB games, Participants accept and agree to adhere to the Rules and the
                            Terms.</li>
                        <li>Leader Board competitions have specified start and end dates as depicted in the Leader Board
                            sections.</li>
                    </ul>
                    RULES
                    <ul>
                        <li>
                            Number of entries per person per game is limited to the amount as determined by the
                            business.
                        </li>
                        <li>
                            You must be 18 years or older to play.
                        </li>
                        <li>A valid email address, cell phone number and/or ID/Passport number may be required to enter
                            BIAB games as determined by the business.</li>
                        <li>
                            Access may require you to provide additional information, such as a username, password, and
                            leader board display name.
                        </li>
                        <li>
                            All entries received after the official cut off time displayed, will be null and void.
                        </li>
                        <li>Only completed entries received that adhere to the Rules and the Terms and are submitted on
                            the official BIAB application, will be considered.</li>
                        <li>
                            Additional rules pertaining to both the Leader Board competitions and the games specified in
                            the relevant sections, are to be adhered to.
                        </li>
                    </ul>
                    <br>
                    TERMS
                    <br>
                    Entries
                    <br>
                    <ul>
                        <li>Costs/Entry fees to enter games can be determined by the business owner.

                        </li>
                        <li>
                            Entries received after the game has started, will not be considered.
                        </li>
                        <li>
                            Neither Dev I.T. nor the business accepts no responsibility for any entries that are
                            incomplete, corrupt or fail to be recorded for any reason whatsoever by the required closing
                            date and time.
                        </li>
                        <li>Any proof of ‘posting’ or ‘sending’ shall not constitute proof of receipt.</li>
                        <li>
                            Automatically generated entries or entries via agents or third parties are invalid and shall
                            not be considered.
                        </li>
                        <li>
                            The business reserves the right to change the Rules and the Terms, without notice.
                        </li>
                    </ul>
                    <br>
                    WINNERS
                    <br>
                    <ul>
                        <li>
                            The business will offer prizes for each game.
                        </li>
                        <li>Prizes for the games are specified within the relevant games.</li>
                        <li>
                            The awarding of prizes is entirely at the discretion of the business.
                        </li>
                        <li>
                            The business reserves the right to cancel the awarding of prizes if it suspects fraudulent
                            conduct and/or dishonesty.
                        </li>
                        <li>
                            Only complete and verified entries will be eligible for prizes.
                        </li>
                        <li>
                            Games prizes will not be carried over if there are no winners in a certain game.
                        </li>
                        <li>
                            The business is under no obligation to notify players of the results of any game. The
                            outcome of any game result can be obtained from management of the business.
                        </li>
                        <li>
                            Leader Board competitions have specified winning prizes.
                        </li>
                        <li>
                            Leader Board competitions are based on cumulative points accrued over the specified time.
                        </li>
                        <li>
                            In the event of a tie in the Leader Board competitions, the specified winning prize amount
                            will be shared equally between the winners.
                        </li>
                        <li>
                            Should the business be unable to contact the winners within three days, the prizes are
                            forfeited.
                        </li>
                        <li>
                            Winners will be notified, provided they supplied accurate contact information.
                        </li>
                        <li>
                            Winners shall comply with all FICA requirements, if necessary.
                        </li>
                        <li>
                            The business may require winners to complete and submit an information disclosure agreement
                            to enable the business to ensure compliance with the Rules and the Terms. Should winners be
                            found not to have complied with the Rules and the Terms, they will automatically be
                            disqualified, and the prizes forfeited. Winners may also be required to sign acceptance of
                            prizes and indemnity documents.
                        </li>
                        <li>
                            Prizes are not transferable, and no substitution of prizes is permitted. Prizes will not be
                            paid to a third party, but only to the verified prize winner.
                        </li>
                        <li>
                            The business reserve the right, at its sole discretion, to substitute prizes with any other
                            prize of comparable or greater commercial value, for whatever reason.
                        </li>
                        <li>
                            The business’ decision is final, and no further correspondence will be entered into.
                        </li>
                    </ul>
                    <br>
                    DISQUALIFIED PARTICIPANTS
                    <br>
                    The following persons are prohibited from participating in BIAB games:
                    <ul>
                        <li>
                            Directors, members, partners, promotional and advertising agents, merchandisers, employees,
                            or consultants of the business.
                            Directors, members, partners, promotional and advertising agents, merchandisers, employees,
                            or consultants of the Dev I.T.
                        </li>
                        <li>
                            Immediate family members of the persons specified above.
                        </li>
                        <li>
                            Persons under the age of 18 years.
                        </li>
                    </ul>
                    <br>
                    PRIVACY
                    <br>
                    <ul>
                        <li>
                            All information provided shall be treated confidential.
                        </li>
                        <li>
                            All information provided by Participants shall be stored securely by the Dev I.T., on behalf
                            of the business and will not be shared with third parties.
                        </li>
                        <li>
                            Participants acknowledge and agree that the business will be required to provide their
                            information and transaction records to any statutory body that is authorized to obtain such
                            information and/or pursuant to an Order of Court.
                        </li>
                        <li>
                            Personal information submitted by Participants will be kept and used in accordance with
                            prevailing South African data protection legislation and privacy policy of the business and
                            Dev I.T.
                        </li>
                        <li>
                            Participants acknowledge and accept that although Dev I.T. and the business shall use its
                            best endeavours to ensure data privacy, neither entity makes any warranty in this regard.
                        </li>
                    </ul>
                    <br>
                    OTHER
                    <br>
                    <ul>
                        <li>
                            All Participants participate entirely at their own risk. Participants indemnify and holds
                            harmless the business, Dev I.T., their directors, employees and agents of any and all
                            liability pertaining to any damage, cost, injuries and losses of whatever nature sustained
                            as a result of their participation in BIAB Games and related events and activities.
                        </li>
                        <li>
                            By entering BIAB Games, Participants authorise the business to collect, store and use (not
                            share) personal information of Participants for communication or statistical purposes.
                        </li>
                        <li>
                            Participants and/or Winners may be required to take part in publicity campaigns for
                            broadcast or publishing purposes. Participants and/or Winners that take part in any
                            publicity will not be entitled to any payment or other form of remuneration. whatsoever for
                            such publicity. Ownership of all publicity materials vest in the business.
                        </li>
                        <li>
                            By entering BIAB Games, Participants and/or Winners grant their consent to the business to
                            publish, feature, reproduce and use their images and photographs for any purpose at any
                            time, in any of its publications, websites and associated media platforms, for no
                            remuneration.
                        </li>
                        <li>
                            The business reserve the right to terminate or extend BIAB games at any time. In the event
                            of such termination or extension, Participants waive any rights that they may have in terms
                            of BIAB games and acknowledge that they will have no recourse whatsoever against the
                            business or Dev I.T.
                        </li>
                        <li>Notwithstanding anything to the contrary contained in these Rules and Terms, the business
                            and Dev I.T. reserve the right to amend, modify, change, postpone suspend or cancel BIAB
                            Games, the Rules and/or the Terms and any prize (which has not yet been allocated), or any
                            aspect thereof, without notice at any time, for any reason which the business or Dev I.T.
                            deem necessary. At the end of the BIAB Games for whatsoever reason, all of the business’ and
                            Dev I.T.’s obligations in regard to the BIAB Game as well as in regard to the awarding of
                            prizes, shall cease.</li>
                    </ul>

                </v-container>
                <!-- <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn @click="dialog = false">close</v-btn>
                </v-card-actions> -->
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
    export default {
        data() {
            return {
                dialog: false,
                text: `









`
            }
        },
        methods: {
            open() {
                let self = this
                self.dialog = true
            },
        }
    }
</script>