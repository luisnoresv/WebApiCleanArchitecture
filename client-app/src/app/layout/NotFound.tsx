import React from 'react';
import { Segment, Button, Header, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const NotFound = () => (
   <Segment placeholder>
      <Header icon>
         <Icon name='search' />
        Oops - we&apos;ve looked everywhere but couldn&apos;t find this.
      </Header>
      <Segment.Inline>
         <Button as={Link} to='/activities' primary>
            Return to Activities page
         </Button>
      </Segment.Inline>
   </Segment>
);

export default NotFound;
