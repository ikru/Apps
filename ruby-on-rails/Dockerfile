# docker build --rm -t ruby-on-rails:0.1 .
# docker rm -f ruby-on-rails
# docker container prune -f
# docker run --rm -p 3000:3000 --name ruby-on-rails ruby-on-rails:0.1
# docker run --rm -p 3000:3000 --name ruby-on-rails -v "$PWD/src":/src -w /src ruby-on-rails:0.1
# docker run -it --rm --name ruby-on-rails -v "$PWD/src":/src -w /src ruby-on-rails:0.1 /bin/bash
# docker exec -it --name ruby-on-rails -v "$PWD/src":/src -w /src ruby-on-rails:0.1 /bin/bash

# to run ruby cmd without the project
# docker build --rm --target rubytools -t rubytools .
# docker run -it --rm --name rubytools -v "$PWD/src":/src -w /src rubytools /bin/bash
FROM ruby:2.7 as rubytools
# RUN apt-get update -qq && apt-get install -y nodejs postgresql-client && \
#    curl -o- -L https://yarnpkg.com/install.sh | bash
# ENV PATH $HOME/.yarn/bin:$HOME/.config/yarn/global/node_modules/.bin:$PATH
# RUN yarn install --check-files
RUN gem install rails
# RUN rails webpacker:install

FROM ruby:2.7 as build
# RUN apt-get update -qq && apt-get install -y nodejs postgresql-client && \
#    curl -o- -L https://yarnpkg.com/install.sh | bash
# ENV PATH $HOME/.yarn/bin:$HOME/.config/yarn/global/node_modules/.bin:$PATH
# RUN yarn install --check-files
WORKDIR /src
COPY src/Gemfile /src/Gemfile
# COPY src/Gemfile.lock /src/Gemfile.lock
RUN bundle install
COPY src/ /src
EXPOSE 3000

# Start the main process.
FROM build
CMD ["rails", "server", "-b", "0.0.0.0"]