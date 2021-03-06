﻿// @source Validation.js

(function () {
    var validation = {
        isNull: function (value) {
            return !Bridge.isDefined(value, true);
        },

        isEmpty: function (value) {
            return value == null || value.length === 0 || Bridge.is(value, Bridge.ICollection) ? value.getCount() == 0 : false;
        },

        isNotEmptyOrWhitespace: function (value) {
            return Bridge.isDefined(value, true) && !(/^$|\s+/.test(value));
        },

        isNotNull: function (value) {
            return Bridge.isDefined(value, true);
        },

        isNotEmpty: function (value) {
            return !Bridge.Validation.isEmpty(value);
        },

        email: function (value) {
            var re = /^(")?(?:[^\."])(?:(?:[\.])?(?:[\w\-!#$%&'*+/=?^_`{|}~]))*\1@(\w[\-\w]*\.){1,5}([A-Za-z]){2,6}$/;

            return re.test(value);
        },

        url: function (value) {
            var re = /(((^https?)|(^ftp)):\/\/((([\-\w]+\.)+\w{2,3}(\/[%\-\w]+(\.\w{2,})?)*(([\w\-\.\?\\\/+@&#;`~=%!]*)(\.\w{2,})?)*)|(localhost|LOCALHOST))\/?)/i;

            return re.test(value);
        },

        alpha: function (value) {
            var re = /^[a-zA-Z_]+$/;

            return re.test(value);
        },

        alphaNum: function (value) {
            var re = /^[a-zA-Z_]+$/;

            return re.test(value);
        },

        creditCard: function (value, type) {
            var re,
                checksum,
                i,
                digit;

            if (type == "Visa") {
                // Visa: length 16, prefix 4, dashes optional.
                re = /^4\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
            } else if (type == "MasterCard") {
                // Mastercard: length 16, prefix 51-55, dashes optional.
                re = /^5[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$/;
            } else if (type == "Discover") {
                // Discover: length 16, prefix 6011, dashes optional.
                re = /^6011-?\d{4}-?\d{4}-?\d{4}$/;
            } else if (type == "AmericanExpress") {
                // American Express: length 15, prefix 34 or 37.
                re = /^3[4,7]\d{13}$/;
            } else if (type == "DinersClub") {
                // Diners: length 14, prefix 30, 36, or 38.
                re = /^3[0,6,8]\d{12}$/;
            } else {
                // Basing min and max length on
                // http://developer.ean.com/general_info/Valid_Credit_Card_Types
                if (!value || value.length < 13 || value.length > 19) {
                    return false;
                }

                re = /[^0-9 \-]+/;
            }

            if (!re.test(value)) {
                return false;
            }

            // Remove all dashes for the checksum checks to eliminate negative numbers
            value = value.split("-").join("");

            // Checksum ("Mod 10")
            // Add even digits in even length strings or odd digits in odd length strings.
            checksum = 0;

            for (i = (2 - (value.length % 2)) ; i <= value.length; i += 2) {
                checksum += parseInt(value.charAt(i - 1));
            }

            // Analyze odd digits in even length strings or even digits in odd length strings.
            for (i = (value.length % 2) + 1; i < value.length; i += 2) {
                digit = parseInt(value.charAt(i - 1)) * 2;

                if (digit < 10) {
                    checksum += digit;
                } else {
                    checksum += (digit - 9);
                }
            }

            return (checksum % 10) == 0;
        }
    };

    Bridge.Validation = validation;
})();
