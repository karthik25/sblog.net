$(function () {
    if ($('.dsq-brlink').length === 0) {
        fixLimelightZone();   
    }    
});

function fixLimelightZone() {
    var offset = $('.limelight').offset();
    var footerHeight = $('.limelight').height();
    var height = window.innerHeight;
    if (height - offset.top - footerHeight > 0)
        $('.limelight').css({ 'position': 'absolute', 'bottom': 0, 'width': $('.limelight').width() });
    else
        $('.limelight').css({ 'position': 'static' });
}
