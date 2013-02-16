###############################################################################
#
# Rakefile
#
# This script automates the process of building the GitHub Pages website for
# testing the website locally before deploying to GitHub.
#
# Copyright 2013 ImaginaryRealities, LLC
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to
# deal in the Software without restriction, including without limitation the
# rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
# sell copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRIGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.
#
###############################################################################

require 'rake/clean'

CLEAN.include('./_temp')

CLOBBER.include('./_site')
CLOBBER.include('./css')
CLOBBER.include('./help')
CLOBBER.include('./javascript')

THEME_FILES = FileList.new('./src/themes/normal.less')
CSS_FILES = THEME_FILES.pathmap('%{^./src/themes,./css}X.css')

THEME_IMAGE_FILES = FileList.new('./lib/twitter-bootstrap/img/glyphicons-halflings.png')
THEME_IMAGE_FILES.include('./lib/twitter-bootstrap/img/glyphicons-halflings-white.png')
CSS_IMAGE_FILES = THEME_IMAGE_FILES.pathmap('./css/images/%f')

JAVASCRIPT_SOURCE_FILES = FileList.new('./lib/requirejs/require.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/jquery/jquery.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-transition.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-modal.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-dropdown.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-scrollspy.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-tab.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-tooltip.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-popover.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-alert.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-button.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-collapse.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-carousel.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-typeahead.js')
JAVASCRIPT_SOURCE_FILES.include('./lib/twitter-bootstrap/js/bootstrap-affix.js')
JAVASCRIPT_SOURCE_FILES.include('./src/javascript/**/*.js')
JAVASCRIPT_SOURCE_FILES.include('./src/javascript/**/*.coffee')

HELP_FILES = FileList.new('../MSBuildExtensions/src/Help/Help/**/*.*')
WEBSITE_HELP_FILES = HELP_FILES.pathmap('%{^../MSBuildExtensions/src/Help/Help,./help}p')

desc 'Builds the GitHub Pages website.'
task :default => [:build_css, :compile_javascript, :copy_api_help] do
	sh "jekyll --pygments --no-lsi --safe"
end

task :build_css => ['./css/images'] + CSS_FILES + CSS_IMAGE_FILES

task :compile_javascript => ['./javascript'] + JAVASCRIPT_SOURCE_FILES do
	sh "r.js.cmd -o src/javascript/build.js"
end

task :copy_api_help => ['./help/fti', './help/html', './help/icons', './help/scripts', './help/styles'] + WEBSITE_HELP_FILES

directory './css'

directory './css/images'

directory './help/fti'

directory './help/html'

directory './help/icons'

directory './help/scripts'

directory './help/styles'

directory './javascript'

THEME_FILES.each do |src|
	dest = src.pathmap('%{^./src/themes,./css}X.css')
	file dest => ['./css', src] do
		sh "lessc -yui-compress #{src} #{dest}"
	end
end

THEME_IMAGE_FILES.each do |src|
	dest = src.pathmap('./css/images/%f')
	file dest => ['./css/images', src] do
		cp src, dest
	end
end

rule(/^\.\/help\// => [proc {|t| t.pathmap('%{^./help,../MSBuildExtensions/src/Help/Help}p')}]) do |t|
	cp t.source, t.name
end
