CKEDITOR.plugins.add('localvideo',
{
    init: function (editor) {
        editor.addCommand('insertLocalvideo',
			{
			    exec: function (editor) {
			        wopen('UploadLocalVideo.aspx', 'Upload Local Video', 1050, 500);
			    }
			});
        editor.ui.addButton('Localvideo',
		{
		    label: 'Embed Local Video',
		    command: 'insertLocalvideo',
		    icon: this.path + 'images/icon.png'
		});
        function wopen(url, name, w, h) {
            // Fudge factors for window decoration space.
            // In my tests these work well on all platforms & browsers.
            w += 32;
            h += 96;
            wleft = (screen.width - w) / 2;
            wtop = (screen.height - h) / 2;
            var win = window.open(url, name,
                        'width=' + w + ', height=' + h + ', ' + 'left=' + wleft + ', top=' + wtop + ', ' + 'location=no, menubar=no, ' + 'status=no, toolbar=no, scrollbars=yes, resizable=no');
            // Just in case width and height are ignored
            win.resizeTo(w, h);
            // Just in case left and top are ignored
            win.moveTo(wleft, wtop);
            win.focus();
            return false;
        }
    }
});
