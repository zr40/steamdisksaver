#!/usr/bin/env ruby

require 'yaml'
require 'json'

data = {
	'version' => 1,
	'release' => Time.now.to_i,
	'categories' => YAML.load_file('src/categories.yaml'),
	'engines' => YAML.load_file('src/engines.yaml'),
	'apps' => YAML.load_file('src/apps.yaml'),
}

Dir['src/apps/*.yaml'].each do |app_filename|
	data['apps'][File.basename app_filename, '.yaml'] = YAML.load_file app_filename
end

File.open 'apps.json', 'w' do |file|
 file.write data.to_json :array_nl => "\n", :object_nl => "\n", :indent => "\t"
end
