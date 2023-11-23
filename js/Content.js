//
//  Helper for correct size of Messages page
//
function MessagesMenuWidth() {
    var W = window.innerWidth;
    var W_menu = $('#sidebar-left').outerWidth();
    var w_messages = (W - W_menu) * 16.666666666666664 / 100;
    $('#messages-menu').width(w_messages);
}
//
//  Screensaver function
//  used on locked screen, and write content to element with id - canvas
//
function ScreenSaver() {
    var canvas = document.getElementById("canvas");
    var ctx = canvas.getContext("2d");
    // Size of canvas set to fullscreen of browser
    var W = window.innerWidth;
    var H = window.innerHeight;
    canvas.width = W;
    canvas.height = H;
    // Create array of particles for screensaver
    var particles = [];
    for (var i = 0; i < 25; i++) {
        particles.push(new Particle());
    }
    function Particle() {
        // location on the canvas
        this.location = { x: Math.random() * W, y: Math.random() * H };
        // radius - lets make this 0
        this.radius = 0;
        // speed
        this.speed = 3;
        // random angle in degrees range = 0 to 360
        this.angle = Math.random() * 360;
        // colors
        var r = Math.round(Math.random() * 255);
        var g = Math.round(Math.random() * 255);
        var b = Math.round(Math.random() * 255);
        var a = Math.random();
        this.rgba = "rgba(" + r + ", " + g + ", " + b + ", " + a + ")";
    }
    // Draw the particles
    function draw() {
        // re-paint the BG
        // Lets fill the canvas black
        // reduce opacity of bg fill.
        // blending time
        ctx.globalCompositeOperation = "source-over";
        ctx.fillStyle = "rgba(0, 0, 0, 0.02)";
        ctx.fillRect(0, 0, W, H);
        ctx.globalCompositeOperation = "lighter";
        for (var i = 0; i < particles.length; i++) {
            var p = particles[i];
            ctx.fillStyle = "white";
            ctx.fillRect(p.location.x, p.location.y, p.radius, p.radius);
            // Lets move the particles
            // So we basically created a set of particles moving in random direction
            // at the same speed
            // Time to add ribbon effect
            for (var n = 0; n < particles.length; n++) {
                var p2 = particles[n];
                // calculating distance of particle with all other particles
                var yd = p2.location.y - p.location.y;
                var xd = p2.location.x - p.location.x;
                var distance = Math.sqrt(xd * xd + yd * yd);
                // draw a line between both particles if they are in 200px range
                if (distance < 200) {
                    ctx.beginPath();
                    ctx.lineWidth = 1;
                    ctx.moveTo(p.location.x, p.location.y);
                    ctx.lineTo(p2.location.x, p2.location.y);
                    ctx.strokeStyle = p.rgba;
                    ctx.stroke();
                    //The ribbons appear now.
                }
            }
            // We are using simple vectors here
            // New x = old x + speed * cos(angle)
            p.location.x = p.location.x + p.speed * Math.cos(p.angle * Math.PI / 180);
            // New y = old y + speed * sin(angle)
            p.location.y = p.location.y + p.speed * Math.sin(p.angle * Math.PI / 180);
            // You can read about vectors here:
            // http://physics.about.com/od/mathematics/a/VectorMath.htm
            if (p.location.x < 0) p.location.x = W;
            if (p.location.x > W) p.location.x = 0;
            if (p.location.y < 0) p.location.y = H;
            if (p.location.y > H) p.location.y = 0;
        }
    }
    setInterval(draw, 30);
}

function OpenModalBox(header, inner, bottom) {
    var modalbox = $('#modalbox');
    modalbox.find('.modal-header-name span').html(header);
    modalbox.find('.devoops-modal-inner').html(inner);
    modalbox.find('.devoops-modal-bottom').html(bottom);
    modalbox.fadeIn('fast');
    $('body').addClass("body-expanded");
}
//
//  Close modalbox
//
//
function CloseModalBox() {
    var modalbox = $('#modalbox');
    modalbox.fadeOut('fast', function () {
        modalbox.find('.modal-header-name span').children().remove();
        modalbox.find('.devoops-modal-inner').children().remove();
        modalbox.find('.devoops-modal-bottom').children().remove();
        $('body').removeClass("body-expanded");
    });
}

/*-------------------------------------------------------------------------------------------------------------------------------------------*/
function WinMove() {
    $("div.box").not('.no-drop')
		.draggable({
		    revert: true,
		    zIndex: 2000,
		    cursor: "crosshair",
		    handle: '.box-name',
		    opacity: 0.8
		})
		.droppable({
		    tolerance: 'pointer',
		    drop: function (event, ui) {
		        var draggable = ui.draggable;
		        var droppable = $(this);
		        var dragPos = draggable.position();
		        var dropPos = droppable.position();
		        draggable.swap(droppable);
		        setTimeout(function () {
		            var dropmap = droppable.find('[id^=map-]');
		            var dragmap = draggable.find('[id^=map-]');
		            if (dragmap.length > 0 || dropmap.length > 0) {
		                dragmap.resize();
		                dropmap.resize();
		            }
		            else {
		                draggable.resize();
		                droppable.resize();
		            }
		        }, 50);
		        setTimeout(function () {
		            draggable.find('[id^=map-]').resize();
		            droppable.find('[id^=map-]').resize();
		        }, 250);
		    }
		});
}
