// @source Boolean.js

Bridge.define('Bridge.Boolean', {
    statics: {
        parse: function (str) {
            if (str == null) {
                throw new Bridge.ArgumentNullException("str");
            }

            if (/^\s*false\s*$/i.test(str)) {
                return false;
            }

            if (/^\s*true\s*$/i.test(str)) {
                return true;
            }

            throw new Bridge.FormatException("Input string was not in a correct format.");            
        },

        tryParse: function (str, result) {
            result.v = false;

            if (/^\s*false\s*$/i.test(str)) {
                result.v = false;
                return true;
            }
            else if (/^\s*true\s*$/i.test(str)) {
                result.v = true;
                return true;
            }

            return false;
        },
    }
});