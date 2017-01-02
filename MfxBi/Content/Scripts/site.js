/// <summary>
/// Root object for any script function used throughout the application
/// </summary>
var Mfxbi = Mfxbi || {};
Mfxbi.RootServer = "";        // Should be set to /vdir when deployed

/// <summary>
/// Return a root-based path
/// </summary>
Mfxbi.fromServer = function (relativeUrl) {
    return Mfxbi.RootServer + relativeUrl;
};

/// <summary>
/// Helper function to post the content of a HTML form
/// </summary>
Mfxbi.postForm = function (formSelector, success, error) {
    var form = $(formSelector);
    $.ajax({
        cache: false,
        url: form.attr('action'),
        type: form.attr('method'),
        dataType: 'html',
        data: form.serialize(),
        success: success,
        error: error
    });
};

/// <summary>
/// Helper function to call a remote URL (POST)
/// </summary>
Mfxbi.post = function (url, data, success, error) {
    $.ajax({
        cache: false,
        url: Mfxbi.fromServer(url),
        type: 'post',
        data: data,
        success: success,
        error: error
    });
};