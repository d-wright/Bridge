﻿/* global Bridge */

"use strict";

Bridge.define('Generics.GenericClass$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.GenericINamedEntity$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.GenericNamedEntity$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.GenericNew$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.GenericNewAndClass$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.GenericStruct$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.implementation', {
    statics: {
        config: {
            init: function () {
                this.simpleGenericInt = new Generics.SimpleGeneric$1(Bridge.Int)(1);
                this.simpleDoubleGenericIntString = new Generics.SimpleDoubleGeneric$2(Bridge.Int,String)("constructor");
                this.genericINamedEntity = new Generics.GenericINamedEntity$1(Generics.INamedEntity)(new Generics.NamedEntity());
                this.genericNamedEntity = new Generics.GenericNamedEntity$1(Generics.NamedEntity)(new Generics.NamedEntity());
                this.genericClassObject = new Generics.GenericClass$1(Object)(2);
                this.genericClassNamedEntity = new Generics.GenericClass$1(Generics.NamedEntity)(new Generics.NamedEntity());
                this.genericNew = new Generics.GenericNew$1(Generics.NewClass)(new Generics.NewClass());
                this.genericNewAndClass = new Generics.GenericNewAndClass$1(Generics.NewClass)(new Generics.NewClass());
            }
        }
    }
});

Bridge.define('Generics.INamedEntity', {
    config: {
        properties: {
            Name: null
        }
    }
});

Bridge.define('Generics.NamedEntity', {
    inherits: [Generics.INamedEntity],
    config: {
        properties: {
            Name$1: null
        }
    }
});

Bridge.define('Generics.NewClass', {
    data: 0,
    constructor: function () {
        this.data = 30;
    }
});

Bridge.define('Generics.SimpleDoubleGeneric$2', function (T, K) { return {
    instanceT: null,
    instanceK: null,
    constructor: function () {
    },
    constructor$1: function (instanceT, instanceK) {
        this.instanceT = instanceT;
        this.instanceK = instanceK;
    },
    getSomething: function (input) {
        return input;
    },
    getSomethingMore: function (input) {
        return input;
    }
}; });

Bridge.define('Generics.SimpleGeneric$1', function (T) { return {
    instance: null,
    constructor: function (instance) {
        this.instance = instance;
    },
    getSomething: function (input) {
        return input;
    }
}; });

