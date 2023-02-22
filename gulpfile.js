const { src, dest, watch, series } = require('gulp');
const sass = require('gulp-sass')(require('sass'));
//const postcss = require('gulp-postcss');
//const cssnano = require('cssnano');
const terser = require('gulp-terser');
// const rename = require('gulp-rename');
var concat = require('gulp-concat');
var minifyCSS = require('gulp-minify-css');
// var bundle = require('gulp-bundle');
const browsersync = require('browser-sync').create();

// Sass Task
function scssTask(){
  return src('wwwroot/scss/*.scss')
    .pipe(sass())
    //.pipe(postcss([cssnano()]))
    // .pipe(rename({ extname: '.min.js' }))
    .pipe(dest('wwwroot/css'));
}
function bundleCss() {
  return src('wwwroot/css/*.css')
          .pipe(minifyCSS())
          .pipe(concat('style.min.css'))
         .pipe(dest('wwwroot/bundle'))
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
                 browsersyncReload,
                 bundleCss
                ));
}

// Default Gulp task
exports.default = series(
  scssTask,
  //jsTask,
  browsersyncServe,
  watchTask,
  bundleCss
);