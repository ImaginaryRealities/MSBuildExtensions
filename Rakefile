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

CLOBBER.include('./_site')
CLOBBER.include('./css')

THEME_FILES = FileList.new('./src/themes/normal.less')
CSS_FILES = THEME_FILES.pathmap('%{^./src/themes,./css}X.css')

THEME_IMAGE_FILES = FileList.new('./lib/twitter-bootstrap/img/glyphicons-halflings.png')
THEME_IMAGE_FILES.include('./lib/twitter-bootstrap/img/glyphicons-halflings-white.png')
CSS_IMAGE_FILES = THEME_IMAGE_FILES.pathmap('./css/images/%f')

desc 'Builds the GitHub Pages website.'
task :default => [:build_css] do
	sh "jekyll --pygments --no-lsi --safe"
end

task :build_css => ['./css/images'] + CSS_FILES + CSS_IMAGE_FILES

directory './css'

directory './css/images'

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
