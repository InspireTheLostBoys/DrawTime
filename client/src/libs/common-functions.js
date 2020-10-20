import moment from 'moment'
class Common {
    constructor() {

    }

    FormatDate(date) {
        return moment(date).format('YYYY-MM-DD');
    }

    FormatTime(time) {
        return moment(time).format('HH:mm');
    }

    FormatDateTime(dateTime) {
        if (dateTime == null)
         return "..."
        return moment(dateTime).format('YYYY-MM-DD HH:mm');
    }

    FormatCustom(dateTime, strFormat) {
        if (dateTime == null)
         return "..."
        return moment(dateTime).format(strFormat);
    }

    FormatDateTimeGMT(dateTime) {
        if (dateTime == null)
         return "..."
        return moment(dateTime).format('YYYY-MM-DD HH:mm ZZ');
    }

    GetDateAgo(date, days) {
        date.setDate(date.getDate() - days);
        return date;
    }
    
    GetUserID() {
        let userData = localStorage.getItem("USER_ID");
        if (userData == null || userData == undefined || userData.length == 0) {
            return false;
        }
        return userData;
    }

    ToCurrency(value) {
        var formatter = new Intl.NumberFormat('en-ZA', {
            style: 'currency',
            currency: 'ZAR',
            minimumFractionDigits: 2
        });

        return formatter.format(value);
    }

    Guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }
        return (
            s4() +
            s4() +
            "-" +
            s4() +
            "-" +
            s4() +
            "-" +
            s4() +
            "-" +
            s4() +
            s4() +
            s4()
        );
    }
}

export default {
    install(Vue) {
        let handler = new Common();

        Vue.prototype.FormatDate = (date) => {
            return handler.FormatDate(date);
        };

        Vue.prototype.FormatTime = (time) => {
            return handler.FormatTime(time);
        };

        Vue.prototype.FormatDateTime = (dateTime) => {
            return handler.FormatDateTime(dateTime);
        };

        Vue.prototype.FormatCustom = (dateTime, format) => {
            return handler.FormatCustom(dateTime, format);
        };

        Vue.prototype.FormatDateTimeGMT = (dateTime) => {
            return handler.FormatDateTimeGMT(dateTime);
        };

        Vue.prototype.GetDateAgo = (dateTime, days) => {
            return handler.GetDateAgo(dateTime, days);
        };

        Vue.prototype.GetUserID = () => {
            return handler.GetUserID();
        };

        Vue.prototype.ToCurrency = (value) => {
            return handler.ToCurrency(value);
        };

        Vue.prototype.guid = () => {
            return handler.Guid();
        };
    }
}