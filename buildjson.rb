#!/usr/bin/env ruby

require 'yaml'
require 'json'

File.open 'apps.json', 'w' do |file|
 file.write YAML.load_file('apps.yaml').to_json
end
