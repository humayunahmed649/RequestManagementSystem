

//Display an asterisk on required fields

$('input[type=text]').each(function () {

            var req = $(this).attr('data-val-required');
            if (undefined != req) {
                var label = $('label[for="' + $(this).attr('id') + '"]');
                var text = label.text();
                if (text.length > 0) {
                    label.append('<span style="color:red"> *</span>');
                }
            }
        });
