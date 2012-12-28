#!/usr/bin/env ruby

require 'yaml'
require 'json'

data = YAML.load_file 'apps.yaml'
data['release'] = Time.now.to_i

File.open 'apps.json', 'w' do |file|
 file.write data.to_json
end
