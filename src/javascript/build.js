/*
 * build.js
 *
 * This JavaScript module is used by the RequireJS optimizer to combine the
 * JavaScript modules and minify them in order to optimize download times for
 * the JavaScript modules by web pages and reduce the number of components that
 * need to be downloaded at runtime by the web browser.
 *
 * Copyright 2013 ImaginaryRealities, LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRIGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 */

{
	appDir: '.',
	baseUrl: '.',
	paths: {
		'bootstrap-transition': '../../lib/twitter-bootstrap/js/bootstrap-transition',
		'bootstrap-modal': '../../lib/twitter-bootstrap/js/bootstrap-modal',
		'bootstrap-dropdown': '../../lib/twitter-bootstrap/js/bootstrap-dropdown',
		'bootstrap-scrollspy': '../../lib/twitter-bootstrap/js/bootstrap-scrollspy',
		'bootstrap-tab': '../../lib/twitter-bootstrap/js/bootstrap-tab',
		'bootstrap-tooltip': '../../lib/twitter-bootstrap/js/bootstrap-tooltip',
		'bootstrap-popover': '../../lib/twitter-bootstrap/js/bootstrap-popover',
		'bootstrap-alert': '../../lib/twitter-bootstrap/js/bootstrap-alert',
		'bootstrap-button': '../../lib/twitter-bootstrap/js/bootstrap-button',
		'bootstrap-collapse': '../../lib/twitter-bootstrap/js/bootstrap-collapse',
		'bootstrap-carousel': '../../lib/twitter-bootstrap/js/bootstrap-carousel',
		'bootstrap-typeahead': '../../lib/twitter-bootstrap/js/bootstrap-typeahead',
		'bootstrap-affix': '../../lib/twitter-bootstrap/js/bootstrap-affix',
		'jquery': '../../lib/jquery/jquery',
		'requirejs': '../../lib/requirejs/require'
	},
	shim: {
		'bootstrap-popover': {
			deps: ['bootstrap-tooltip']
		},
		'bootstrap-collapse': {
			deps: ['bootstrap-transition']
		}
	},
	dir: '../../javascript',
	locale: 'en-us',
	optimize: 'uglify2',
	optimizeCss: 'none',
	skipDirOptimize: true,
	uglify2: {
		warnings: true,
		mangle: false
	},
	generateSourceMaps: false,
	preserveLicenseComments: true,
	modules: [
		{
			name: 'website',
			include: ['requirejs', 'jquery', 'bootstrap-dropdown']
		},
		{
			name: 'homepage',
			exclude: ['website']
		}
	]
}