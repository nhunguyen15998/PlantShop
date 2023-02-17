const { src, dest, watch, series } = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const postcss = require('gulp-postcss');
const cssnano = require('cssnano');
const terser = require('gulp-terser');
const browsersync = require('browser-sync').create();

// Sass Task
function scssTask(){
  return src('wwwroot/scss/*.scss', { sourcemaps: true })
    .pipe(sass())
    .pipe(postcss([cssnano()]))
    .pipe(dest('wwwroot/css', { sourcemaps: '.' }));
}

// JavaScript Task
// function jsTask(){
//   return src('wwwroot/js/*.js', { sourcemaps: true })
//     .pipe(terser())
//     .pipe(dest('wwwroot/js', { sourcemaps: '.' }));
// }

// Browsersync Tasks
function browsersyncServe(cb){
  browsersync.init({
    server: {
      baseDir: '.'
    }
  });
  cb();
}

function browsersyncReload(cb){
  browsersync.reload();
  cb();
}

// Watch Task
function watchTask(){
  watch('*.html', browsersyncReload);
  watch(['wwwroot/scss/*.scss', 'wwwroot/js/*.js'], 
          series(scssTask, 
                 //jsTask, 
                 browsersyncReload
                ));
}

// Default Gulp task
exports.default = series(
  scssTask,
  // jsTask,
  browsersyncServe,
  watchTask
);