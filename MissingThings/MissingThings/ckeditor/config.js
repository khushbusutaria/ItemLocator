/**
* @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
* For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.extraPlugins = 'youtube,imagebrowser,localvideo';
    config.youtube_width = '640';
    config.youtube_height = '480';
    config.youtube_related = true;
    config.youtube_older = false;
    config.youtube_privacy = false;
    config.filebrowserBrowseUrl = "CkeditorPlugin/BrowseImage.aspx";
    CKEDITOR.config.allowedContent = true;

    //    config.toolbar = 'AdminPanel';
    config.toolbar_AdminPanel = [
    { name: 'document', items: ['Source', 'Preview', 'Templates'] },
	{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	{ name: 'editing', items: ['Find', 'Replace', '-', 'SpellChecker', 'Scayt'] },
    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Youtube', 'Localvideo'] },

	{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote',
	'-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
	},
	{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },


	{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	{ name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
    ];



    //    config.toolbar = 'ClientSide';

    config.toolbar_ClientSide = [
    { name: 'document', items: ['Source', 'Preview', 'Templates'] },
	{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo'] },
	{ name: 'editing', items: ['SpellChecker', 'Scayt'] },

	{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote',
	'-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
	},
	{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },


	{ name: 'styles', items: ['Format'] },
	{ name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
    ];



    //    config.toolbar = [
    //    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
    //	{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
    //	{ name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
    //	{ name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',
    //        'HiddenField']
    //	},
    //	'/',
    //	{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
    //	{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
    //	'-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
    //	},
    //	{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
    //	{ name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
    //	'/',
    //	{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
    //	{ name: 'colors', items: ['TextColor', 'BGColor'] },
    //	{ name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
    //];






    //    config.toolbarGroups = [
    //    { name: 'clipboard', groups: ['clipboard', 'undo'] },
    //    { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
    //    { name: 'links' },
    //    { name: 'insert' },
    //    { name: 'forms' },
    //    { name: 'tools' },
    //    { name: 'document', groups: ['mode', 'document', 'doctools'] },
    //    { name: 'others' },
    //    '/',
    //    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
    //    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align'] },
    //    { name: 'styles' },
    //    { name: 'colors' },
    //    { name: 'about' }
    //];

};
