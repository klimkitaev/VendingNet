/// <reference path="jquery-1.9.1.js" />
(function(){

    var enter_money = function(stype) {
        $('#allert-container').html('');
        $.get("Home/EnterMoney", { "s_type": stype }, function (data) {
            if (data == true) {
                get_money_cache();
                load_user_wallet();
                load_vm_wallet();
            }
        }, 'json');
    }

    var buy_product = function(ptype) {
        $('#allert-container').html('');
        $.get("Home/Buy", { "p_type": ptype }, function (data) {
            $('#allert-container').html(data);
            load_vm_wallet();
            load_product_catalog();
            get_money_cache();
        });
    }

     var get_change = function() {
        $('#allert-container').html('');
        $.get("Home/GetChange", function (data) {
            get_money_cache();
            load_user_wallet();
            load_vm_wallet();
        }, 'json');
    }

     var get_money_cache = function () {
        $.get("Home/GetMoneyCache", function (data) {
            $('#entered_summ').text(data)
        }, 'json');
    }

     var load_vm_wallet = function () {
        $.get("Home/GetVMWallet", function (data) {
            $('#vwWallet_container').html(data);
        });
    }

     var load_user_wallet = function () {
        $.get("Home/GetUserWallet", function (data) {
            $('#userWallet_container').html(data);
            $('.enter-button').click(function () {
                var stype = $(this).attr("data-face");
                enter_money(stype);
            });
        });
    }

     var load_product_catalog = function () {
        $.get("Home/GetProductCatatalog", function (data) {
            $('#products_container').html(data);
            $('.buy-button').click(function () {
                var ptype = $(this).attr("data-face");
                buy_product(ptype);
            });
        });
    }

    
    $(document).ready(function () {
        load_vm_wallet();
        load_user_wallet();
        load_product_catalog();
    
        $('#btnChange').click(function () {
            get_change();
        })
    });

})();